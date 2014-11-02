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
            ctx.Teams.RemoveRange(ctx.Teams);
            ctx.Matches.RemoveRange(ctx.Matches);

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

            ctx.Matches.Add(new Match()
            {
                HomeTeam = england,
                AwayTeam = uruguay,
                GoalsHomeTeam = 1,
                GoalsAwayTeam = 3,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(50, 0, 0, 0))
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = italy,
                AwayTeam = costarica,
                GoalsHomeTeam = 1,
                GoalsAwayTeam = 1,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(50, 0, 0, 0))
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = england,
                AwayTeam = italy,
                GoalsHomeTeam = 0,
                GoalsAwayTeam = 2,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(45, 0, 0, 0))
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = uruguay,
                AwayTeam = costarica,
                GoalsHomeTeam = 1,
                GoalsAwayTeam = 0,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(45, 0, 0, 0))
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = england,
                AwayTeam = costarica,
                GoalsHomeTeam = 0,
                GoalsAwayTeam = 3,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(41, 0, 0, 0))
            });

            ctx.Matches.Add(new Match()
            {
                HomeTeam = italy,
                AwayTeam = uruguay,
                GoalsHomeTeam = 1,
                GoalsAwayTeam = 3,
                MatchDate = DateTime.Now.Subtract(new TimeSpan(41, 0, 0, 0))
            });

            ctx.SaveChanges();
        }
    }
}