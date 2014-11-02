using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScoreTracker.API.Model;
using ScoreTracker.API.Model.Repo;

namespace ScoreTracker.API.Controllers
{
    [RoutePrefix("api/default")]
    public class TeamController : ApiController
    {
        private readonly IRepository<Team> teamRepository; 

        public TeamController()
        {
            teamRepository = new DbRepository<Team>(new ScoreTrackerContext());
        }

        public IEnumerable<Team> Get()
        {
            return teamRepository.Get().ToList();
        }

        public Team Get(int id)
        {
            return teamRepository.GetById(id);
        } 
    }
}
