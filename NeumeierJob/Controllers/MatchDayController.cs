using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NeumeierJob.Models;
using System.Threading.Tasks;
using NeumeierJob.Util;

namespace NeumeierJob.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class MatchDayController : ControllerBase
    {
        private readonly ILogger<MatchDayController> _logger;
        private readonly IFileLoader _fileloader;

        string filecontent = string.Empty;

        public MatchDayController(ILogger<MatchDayController> logger, IFileLoader fileloader)
        {
            _logger = logger;
            _fileloader = fileloader;

            Load(@"2019.json").Wait();
        }

        async Task Load(string name)
        {
            filecontent = await _fileloader.LoadAsync(@"2019.json"); 
        }
                
        // gibt Text der serialisierten Match-Objekte zurück!
        IEnumerable<Match> GetMatchDay(int id)
        {            
            if (id > 0 && id <= 34)
            {
                var result = JsonConvert.DeserializeObject<List<Match>>(filecontent).Where(x => x.Group.GroupOrderID == id);
                
                if (result == null)
                    return Enumerable.Empty<Match>();

                result.ToList().ForEach(m => _logger.LogInformation($"{m.Group.GroupOrderID}: {m.MatchDateTime}: ({m.Team1.ShortName}) {m.Team1.TeamName} : ({m.Team2.ShortName}) {m.Team2.TeamName}" +
                    $" {m.MatchResults[1].PointsTeam1}:{m.MatchResults[1].PointsTeam2}"));

                return result;
            }
            return Enumerable.Empty<Match>();
        } 

        // Raw Text von allen Spielen
        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("GET");
            return filecontent;
        }

        // Serialisierten Text von Klasse Match
        [HttpGet("{matchday}")]
        public string Get(int matchday)
        {
            _logger.LogInformation($"GET {matchday}");
            
            var matches = GetMatchDay(matchday);
            if (matches.Count() == 0)
                return "Problem!";

            string result = JsonConvert.SerializeObject(matches);

            return result;
        }

        // Serialisierten Text von Klasse Match
        [HttpGet("{year}/{matchday}")]
        public string Get(int year, int matchday)
        {
            _logger.LogInformation($"GET {year}/{matchday}");

            var matches = GetMatchDay(matchday);
            if (matches.Count() == 0)
                return "Problem!";

            string result = JsonConvert.SerializeObject(matches);
            return result;
        }
    }

}
