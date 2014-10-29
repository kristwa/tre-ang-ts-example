using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScoreTracker.API.Controllers
{
    [RoutePrefix("api/default")]
    public class DefaultController : ApiController
    {
        public string Get()
        {
            return "I'm alive!";
        }
    }
}
