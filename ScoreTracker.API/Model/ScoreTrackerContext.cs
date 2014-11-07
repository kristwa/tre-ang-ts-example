using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScoreTracker.API.Model
{

    public class ScoreTrackerContext : DbContext
    {
        public ScoreTrackerContext() : base("ScoreTrackerConnection")
        {
            
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches  { get; set; }
        public DbSet<Championship> Championships { get; set; }
        public DbSet<Group> Groups { get; set; }
        
    }

    public class ScoreTrackerConfiguration : DbConfiguration
    {
        public ScoreTrackerConfiguration()
        {
            SetDatabaseInitializer(new ScoreTrackerInitializer());
        }
    }

    public class ScoreTrackerInitializer : DropCreateDatabaseIfModelChanges<ScoreTrackerContext>
    {
        protected override void Seed(ScoreTrackerContext ctx)
        {
            ctx.Matches.RemoveRange(ctx.Matches);
            ctx.Teams.RemoveRange(ctx.Teams);
            ctx.Groups.RemoveRange(ctx.Groups);
            ctx.Championships.RemoveRange(ctx.Championships);

            ctx.SaveChanges();

            var wc2015 = new Championship()
            {
                ChampionshipName = "World Cup 2015"
            };

            ctx.Championships.Add(wc2015);
            ctx.SaveChanges();

            var england = new Team()
            {
                TeamName = "England"
            };
            var uruguay = new Team()
            {
                TeamName = "Uruguay"
            };
            var italy = new Team()
            {
                TeamName = "Italy"
            };
            var costarica = new Team()
            {
                TeamName = "Costa Rica"
            };

            ctx.Teams.Add(england);
            ctx.Teams.Add(uruguay);
            ctx.Teams.Add(italy);
            ctx.Teams.Add(costarica);

            ctx.SaveChanges();

            var group1 = new Group()
            {
                Championship = wc2015,
                Name = "A"
            };

            var group2 = new Group()
            {
                Championship = wc2015,
                Name = "B"
            };

            group1.Teams.Add(england);
            group1.Teams.Add(italy);
            group1.Teams.Add(uruguay);
            group1.Teams.Add(costarica);

            ctx.Groups.Add(group1);
            ctx.Groups.Add(group2);

            ctx.SaveChanges();
            

            ctx.Matches.Add(new Match()
            {
                HomeTeam = england,
                AwayTeam = uruguay,
                GoalsHomeTeam = 1,
                GoalsAwayTeam = 3,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(50, 0, 0, 0)),
                Group = group1
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = italy,
                AwayTeam = costarica,
                GoalsHomeTeam = 1,
                GoalsAwayTeam = 1,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(50, 0, 0, 0)),
                Group = group1
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = england,
                AwayTeam = italy,
                GoalsHomeTeam = 0,
                GoalsAwayTeam = 2,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(45, 0, 0, 0)),
                Group = group1
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = uruguay,
                AwayTeam = costarica,
                GoalsHomeTeam = 1,
                GoalsAwayTeam = 0,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(45, 0, 0, 0)),
                Group = group1
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = england,
                AwayTeam = costarica,
                GoalsHomeTeam = 0,
                GoalsAwayTeam = 3,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(41, 0, 0, 0)),
                Group = group1
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = italy,
                AwayTeam = uruguay,
                GoalsHomeTeam = 1,
                GoalsAwayTeam = 3,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(41, 0, 0, 0)),
                Group = group1
            });

            ctx.SaveChanges();
        }
    }
}