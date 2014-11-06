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
    public class TeamsController : ApiController
    {
        private readonly IRepository<Team> _teamRepository; 

        public TeamsController(IRepository<Team> teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public IEnumerable<Team> Get()
        {
            return _teamRepository.Get().ToList();
        }

        public Team Get(int id)
        {
            return _teamRepository.GetById(id);
        } 
    }
}
