using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeumeierJob.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string ShortName { get; set; }
        public string TeamName { get; set; }
        public string TeamIconUrl { get; set; }
    }
}
