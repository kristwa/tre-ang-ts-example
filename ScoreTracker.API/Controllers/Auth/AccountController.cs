using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ScoreTracker.API.Model;
using ScoreTracker.API.Model.Repo;

namespace ScoreTracker.API.Controllers.Auth
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private readonly IAuthRepository _repo;

        public AccountController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [Authorize(Roles = "MainUser")]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword([FromBody]string newpasswordText)
        {
            await _repo.ResetPassword(GetUserId(), newpasswordText);

            return Ok();
        }


        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _repo.RegisterUser(userModel);

            var errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Helpers
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }


        internal string GetUserId()
        {
            var identity = User.Identity as ClaimsIdentity;

            if (identity == null)
            {
                return string.Empty;
            }

            return identity.Claims.Where(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid")
                        .Select(c => c.Value)
                        .SingleOrDefault();
        }
        #endregion
    }

}
