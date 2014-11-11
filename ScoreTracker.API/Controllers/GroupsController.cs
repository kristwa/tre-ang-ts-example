using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScoreTracker.API.Model;
using ScoreTracker.API.Model.Repo;
using ScoreTracker.API.Lib;

namespace ScoreTracker.API.Controllers
{
    public class GroupsController : ApiController
    {
        private readonly IRepository<Group> _groupRepository;

        public GroupsController(IRepository<Group> groupRepository )
        {
            _groupRepository = groupRepository;
        }

        public IHttpActionResult GetGroups()
        {
            return Ok(_groupRepository.Get().Include("Matches.HomeTeam").Include("Matches.AwayTeam").ToList());
        }

        public IHttpActionResult GetGroup(int groupId)
        {
            return Ok(_groupRepository.Get().Include("Matches.HomeTeam").Include("Matches.AwayTeam").FirstOrDefault(group => group.Id == groupId));
        }
    }
}
