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
    public class MatchesController : ApiController
    {
        private readonly IRepository<Match> _matchRepository;
        
        public MatchesController(IRepository<Match> matchRepository)
        {
            _matchRepository = matchRepository;
        }

        public IEnumerable<Match> Get()
        {
            return _matchRepository.Get().Include("HomeTeam").Include("AwayTeam");
        }

        public Match Get(int id)
        {
            return _matchRepository.GetById(id);
        }
    }
}
