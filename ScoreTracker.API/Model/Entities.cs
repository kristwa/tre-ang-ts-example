using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ScoreTracker.API.Model
{
    public class Championship
    {
        public Championship()
        {
            Groups = new HashSet<Group>();
        }
        public int Id { get; set; }
        public string ChampionshipName { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }

    public class Group
    {
        public Group()
        {
            Teams = new HashSet<Team>();
            Matches = new HashSet<Match>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        [JsonIgnore]
        public Championship Championship { get; set; }
    }

    public class Team
    {
        public Team()
        {
            Groups = new HashSet<Group>();
        }
        public int Id { get; set; }
        public string TeamName { get; set; }
        public string HomefieldName { get; set; }
        [JsonIgnore]
        public virtual ICollection<Group> Groups { get; set; }
    }

    public class Match
    {
        public int Id { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public int? GoalsHomeTeam { get; set; }
        public int? GoalsAwayTeam { get; set; }
        public DateTime MatchDate { get; set; }
        [JsonIgnore]
        public Group Group { get; set; }
    }
}