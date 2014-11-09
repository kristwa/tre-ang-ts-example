using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScoreTracker.API.Model;
using ScoreTracker.API.Model.Repo;

namespace ScoreTracker.Api.Test
{
    public class MockData<T> where T : class
    {
        List<Championship> championships = new List<Championship>();
 

        public MockData()
        {
            var wc2015 = new Championship()
            {
                ChampionshipName = "World Cup 2015"
            };

            

            var england = new Team()
            {
                TeamName = "England",
                Id = 1
            };
            var uruguay = new Team()
            {
                TeamName = "Uruguay",
                Id = 2
            };
            var italy = new Team()
            {
                TeamName = "Italy",
                Id = 3
            };
            var costarica = new Team()
            {
                TeamName = "Costa Rica",
                Id = 4
            };
            var norway = new Team()
            {
                TeamName = "Norway",
                Id = 5
            };
            var brazil = new Team()
            {
                TeamName = "Brazil",
                Id = 6
            };
            var germany = new Team()
            {
                TeamName = "Germany",
                Id = 7
            };
            var japan = new Team()
            {
                TeamName = "Japan",
                Id = 8
            };

            var group1 = new Group()
            {
                Championship = wc2015,
                Name = "A",
                Id = 1
            };

            var group2 = new Group()
            {
                Championship = wc2015,
                Name = "B",
                Id = 2
            };

            group1.Teams.Add(england);
            group1.Teams.Add(italy);
            group1.Teams.Add(uruguay);
            group1.Teams.Add(costarica);

            group2.Teams.Add(norway);
            group2.Teams.Add(brazil);
            group2.Teams.Add(germany);
            group2.Teams.Add(japan);

            AddMatch(group1, england, uruguay, DateTime.Now.Subtract(new TimeSpan(50, 0, 0, 0)), 1, 3);
            AddMatch(group1, italy, costarica, DateTime.Now.Subtract(new TimeSpan(50, 0, 0, 0)), 1, 1);
            AddMatch(group1, england, italy, DateTime.Now.Subtract(new TimeSpan(45, 0, 0, 0)), 0, 2);
            AddMatch(group1, uruguay, costarica, DateTime.Now.Subtract(new TimeSpan(45, 0, 0, 0)), 1, 0);
            AddMatch(group1, england, costarica, DateTime.Now.Subtract(new TimeSpan(41, 0, 0, 0)), 0, 3);
            AddMatch(group1, italy, uruguay, DateTime.Now.Subtract(new TimeSpan(41, 0, 0, 0)), 1, 3);
            AddMatch(group2, norway, brazil, new DateTime(2014, 10, 10), 1, 4);
            AddMatch(group2, germany, japan, new DateTime(2014, 10, 10), 3, 0);
            AddMatch(group2, japan, norway, new DateTime(2015, 10, 10));
            AddMatch(group2, brazil, germany, new DateTime(2014, 10, 10));
            AddMatch(group2, norway, germany, new DateTime(2014, 11, 11));
            AddMatch(group2, japan, brazil, new DateTime(2014, 11, 12));

            wc2015.Groups.Add(group1);
            wc2015.Groups.Add(group2);
            championships.Add(wc2015);

        }

        private void AddMatch(Group group, Team homeTeam, Team awayTeam, DateTime matchTime, int? goalsHomeTeam = null, int? goalsAwayTeam = null)
        {
            group.Matches.Add(new Match()
            {
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                GoalsAwayTeam = goalsAwayTeam,
                GoalsHomeTeam = goalsHomeTeam,
                MatchDate = matchTime,
                Group = group
            });
        }


        public IQueryable<T> GetData()
        {
            if (typeof (T) == typeof (Championship))
            {
                var wc = (IEnumerable<T>) (object) championships;
                return wc.AsQueryable();
            }
            else if (typeof (T) == typeof (Group))
            {
                var groups = championships.FirstOrDefault().Groups;
                var groupsGen = (IEnumerable<T>) (object) groups;
                return groupsGen.AsQueryable();
            }

            return null;
        }
    }


    public class MockRepository<T> : IRepository<T> where T : class
    {
        private IQueryable<T> _mockData; 
        public MockRepository()
        {
            var mockGen = new MockData<T>();
            _mockData = mockGen.GetData();
        }

        public IQueryable<T> Get()
        {
            return _mockData;
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void AddElement(T element)
        {
            throw new NotImplementedException();
        }

        public void DeleteElement(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<T> elements)
        {
            throw new NotImplementedException();
        }

        public void UpdateElement(T element)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
