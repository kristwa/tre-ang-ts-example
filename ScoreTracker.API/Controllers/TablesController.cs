using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScoreTracker.API.Lib;
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

        public IHttpActionResult GetTable(int groupId)
        {
            var group = _groupRepository.Get().FirstOrDefault(g => g.Id == groupId);
            if (group == null)
                return null;

            var table = _tableGenerator.GenerateTable(group);
            return Ok(table);
        }

        
    }
}
