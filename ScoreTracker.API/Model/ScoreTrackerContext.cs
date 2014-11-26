using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using ScoreTracker.API.Lib;

namespace ScoreTracker.API.Model
{

    public class ScoreTrackerContext : IdentityDbContext<IdentityUser>
    {
        public ScoreTrackerContext() : base("ScoreTrackerConnection")
        {
            
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches  { get; set; }
        public DbSet<Championship> Championships { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
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
            var norway = new Team()
            {
                TeamName = "Norway"
            };
            var brazil = new Team()
            {
                TeamName = "Brazil"
            };
            var germany = new Team()
            {
                TeamName = "Germany"
            };
            var japan = new Team()
            {
                TeamName = "Japan"
            };

            ctx.Teams.Add(england);
            ctx.Teams.Add(uruguay);
            ctx.Teams.Add(italy);
            ctx.Teams.Add(costarica);
            ctx.Teams.Add(norway);
            ctx.Teams.Add(brazil);
            ctx.Teams.Add(germany);
            ctx.Teams.Add(japan);

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

            group2.Teams.Add(norway);
            group2.Teams.Add(brazil);
            group2.Teams.Add(germany);
            group2.Teams.Add(japan);

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

            AddMatch(ctx, group2, norway, brazil, new DateTime(2014, 10, 10), 1, 4);
            AddMatch(ctx, group2, germany, japan, new DateTime(2014, 10, 10), 3, 0);
            AddMatch(ctx, group2, japan, norway, new DateTime(2015, 10, 10));
            AddMatch(ctx, group2, brazil, germany, new DateTime(2014, 10, 10));
            AddMatch(ctx, group2, norway, germany, new DateTime(2014, 11, 11));
            AddMatch(ctx, group2, japan, brazil, new DateTime(2014, 11, 12));

            ctx.SaveChanges();

            ctx.Clients.Add(new Client()
            {
                Id = "scoretrackerApp",
                Name = "Scoretracker",
                Secret = "123123".GetHash(),
                ApplicationType = ApplicationTypes.JavaScript,
                Active = true,
                RefreshTokenLifeTime = 7200,
                AllowedOrigin = "http://localhost:27398"
            });

            ctx.SaveChanges();
            
        }

        private void AddMatch(ScoreTrackerContext ctx, Group group, Team homeTeam, Team awayTeam, DateTime matchTime, int? goalsHomeTeam = null, int? goalsAwayTeam = null)
        {
            ctx.Matches.Add(new Match()
            {
                HomeTeam = homeTeam,
                AwayTeam = awayTeam,
                GoalsAwayTeam = goalsAwayTeam,
                GoalsHomeTeam = goalsHomeTeam,
                MatchDate = matchTime,
                Group = group
            });
        }
    }
}