using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeumeierJob.Models
{    public class Match
    {
        public int MatchID { get; set; }
        public DateTime MatchDateTime { get; set; }

        public Team Team1 { get; set; }
        public Team Team2 { get; set; }

        public Group Group { get; set; }
        public List<MatchResult> MatchResults { get; set; }
    }

    public class MatchResult
    {
        public int PointsTeam1 { get; set; }
        public int PointsTeam2 { get; set; }
        public int ResultTypeID { get; set; }
    }

    public class MatchDay
    {
        public int Day { get; set; }
        public List<Match> Matches { get; set; }
    }

    public class Group
    {
        public string GroupName { get; set; }
        public int GroupOrderID { get; set; }
    }
}
