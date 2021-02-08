using Microsoft.Extensions.Logging;
using NeumeierJob.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NeumeierJob.Util
{
    // via DependencyInjection (siehe Startup)
    internal class Jsonfileloader : IFileLoader
    {
        ILogger<Jsonfileloader> _logger;
        public Jsonfileloader(ILogger<Jsonfileloader> logger)
        {
            if (logger != null)
                _logger = logger;
        }

        public async Task<string> LoadAsync(string name)
        {
            _logger.LogInformation(nameof(Jsonfileloader) + $" loading file async: {name}");

            return await LoadFileAsync(@"2019.json");
        }

        async Task<string> LoadFileAsync(string name)
        {
            string result = string.Empty;
            using (var s = new StreamReader(name))
            {
                if (s != null)
                {
                    result = await s.ReadToEndAsync();
                }
                else
                {
                    _logger.LogError($"File {name} not found!");
                }
            }

            return result;
        }
    }
}
