using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScoreTracker.API.Model;
using ScoreTracker.API.Model.Repo;

namespace ScoreTracker.API.Controllers
{
    public class TablesController : ApiController
    {
        private readonly IRepository<Match> _matchRepository;

        public TablesController(IRepository<Match> matchRepository )
        {
            _matchRepository = matchRepository;
        }

        public IEnumerable<TableItem> GetTable()
        {
            List<TableItem> table;

            var matches = _matchRepository.Get().Include("HomeTeam").Include("AwayTeam");

            var homeTeams = matches.Select(match => match.HomeTeam).Distinct();
            var awayTeams = matches.Select(match => match.AwayTeam).Distinct();

            table = homeTeams.Concat(awayTeams).Distinct().Select(team => new TableItem() { Team = team }).ToList();


            foreach (var teamItem in table)
            {
                foreach (var match in matches.Where(match => match.HomeTeam.Id == teamItem.Team.Id || match.AwayTeam.Id == teamItem.Team.Id))
                {
                    var goalsScored = match.HomeTeam == teamItem.Team ? match.GoalsHomeTeam : match.GoalsAwayTeam;
                    var goalsConceeded = match.HomeTeam == teamItem.Team ? match.GoalsAwayTeam : match.GoalsHomeTeam;

                    var difference = goalsScored - goalsConceeded;

                    teamItem.GoalsFor += goalsScored;
                    teamItem.GoalsAgainst += goalsConceeded;
                    teamItem.Games++;

                    if (difference > 0)
                    {
                        teamItem.Points += 3;
                        teamItem.Won++;
                    }
                    else if (difference == 0)
                    {
                        teamItem.Points += 1;
                        teamItem.Drawn++;
                    }
                    else
                    {
                        teamItem.Lost++;
                    }

                }
            }

            table =
                table.OrderByDescending(team => team.Points).ThenByDescending(team => team.GoalsFor - team.GoalsAgainst).ToList();

            for (int i = 0; i < table.Count; i++)
            {
                table[i].Place = i + 1;
            }


            return table;
        } 
    }
}
