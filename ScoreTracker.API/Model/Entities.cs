using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreTracker.API.Model
{
    public class Team
    {
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string HomefieldName { get; set; }
    }

    public class Match
    {
        public int Id { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int GoalsHomeTeam { get; set; }
        public int GoalsAwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
    }
}