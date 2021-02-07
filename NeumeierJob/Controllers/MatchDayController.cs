using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NeumeierJob.Models;
using System.Text;

namespace NeumeierJob.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class MatchDayController : ControllerBase
    {
        private readonly ILogger<MatchDayController> _logger;
        
        string filecontent = string.Empty;
        
        List<Match> matches = new List<Match>();


        public MatchDayController(ILogger<MatchDayController> logger)
        {
            _logger = logger;

            if(filecontent == string.Empty)
            {
                LoadFile(@"2019.json");
            }

        }

        void LoadFile(string name)
        {
            using (var s = new StreamReader(name))
            {
                if (s != null)
                {
                    _logger.LogInformation($"Loading {name}...");
                    filecontent = s.ReadToEnd();


                    matches = JsonConvert.DeserializeObject<List<Match>>(filecontent);
                    var matchday = matches.Where(x => x.Group.GroupOrderID == 1).ToList();

                    matchday.ForEach((m) =>
                    _logger.LogInformation($"{m.Group.GroupOrderID}: {m.MatchDateTime}: ({m.Team1.ShortName}) {m.Team1.TeamName} : ({m.Team2.ShortName}) {m.Team2.TeamName}" +
                    $" {m.MatchResults[1].PointsTeam1}:{m.MatchResults[1].PointsTeam2}"));
                }
                else
                { 
                    _logger.LogInformation($"File {name} not found...");
                }
            }
        }

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

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("GET");
            return filecontent;
        }

        [HttpGet("{matchday}")]
        public string Get(int matchday)
        {
            _logger.LogInformation($"GET {matchday}");

            var result = GetMatchDay(matchday);
            

            return result;
        }

        [HttpGet("{year}/{matchday}")]
        public string Get(int year, int matchday)
        {
            _logger.LogInformation($"GET {year}/{matchday}");

            return filecontent;
        }
    }

}
