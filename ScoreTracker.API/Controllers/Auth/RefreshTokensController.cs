using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ScoreTracker.API.Model.Repo;

namespace ScoreTracker.API.Controllers.Auth
{
    [RoutePrefix("api/refreshtokens")]
    public class RefreshTokensController : ApiController
    {
        private readonly IAuthRepository _repo;

        public RefreshTokensController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllRefreshTokens());
        }

        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await _repo.RemoveRefreshToken(tokenId);
            if (result)
                return Ok();
            return BadRequest("Token Id does not exist");
        }
    }
}
