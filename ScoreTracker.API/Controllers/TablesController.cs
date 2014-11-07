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
        private readonly IRepository<Group> _groupRepository;
        private readonly ICompetitionTableGenerator _tableGenerator;

        public TablesController(IRepository<Group> groupRepository, ICompetitionTableGenerator tableGenerator)
        {
            _groupRepository = groupRepository;
            _tableGenerator = tableGenerator;
        }

        public IEnumerable<TableItem> GetTable(int groupId)
        {
            List<TableItem> table;

            var group = _groupRepository.Get().FirstOrDefault(g => g.Id == groupId);
            if (group == null)
                return null;

            return _tableGenerator.GenerateTable(group);
        }

        
    }

    public interface ICompetitionTableGenerator
    {
        List<TableItem> GenerateTable(Group group);
    }

    public class CompetitionTableGenerator : ICompetitionTableGenerator
    {

        public List<TableItem> GenerateTable(Group group)
        {
            var table = group.Teams.Select(team => new TableItem() { Team = team }).ToList();

            foreach (var match in group.Matches.Where(match => DateTime.Now >= match.MatchDate))
            {
                var tableItemHomeTeam = table.FirstOrDefault(t => t.Team.Id == match.HomeTeam.Id);
                var tableItemAwayTeam = table.FirstOrDefault(t => t.Team.Id == match.AwayTeam.Id);

                if (!match.GoalsAwayTeam.HasValue || !match.GoalsHomeTeam.HasValue)
                    continue;

                UpdateTable(tableItemHomeTeam, match.GoalsHomeTeam.Value, match.GoalsAwayTeam.Value);
                UpdateTable(tableItemAwayTeam, match.GoalsAwayTeam.Value, match.GoalsHomeTeam.Value);
            }

            table =
                table.OrderByDescending(team => team.Points).ThenByDescending(team => team.GoalsFor - team.GoalsAgainst).ThenByDescending(team => team.GoalsFor).ToList();

            for (int i = 0; i < table.Count; i++)
            {
                table[i].Place = i + 1;
            }

            return table;
        }

        private void UpdateTable(TableItem teamTableItem, int goalsScored, int goalsConceeded)
        {
            teamTableItem.GoalsFor += goalsScored;
            teamTableItem.GoalsAgainst += goalsConceeded;
            teamTableItem.Games++;

            var diff = goalsScored - goalsConceeded;

            if (diff > 0)
            {
                teamTableItem.Won++;
                teamTableItem.Points += 3;
            }
            else if (diff == 0)
            {
                teamTableItem.Drawn++;
                teamTableItem.Points++;
            }
            else
            {
                teamTableItem.Lost++;
            }
        } 
    }
}
