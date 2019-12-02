using Cinema.Persisted.Entities;
using Cinema.Web.Models;
using Cinema.Web.Providers.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/Authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationProvider _authenticationProvider;

        public AuthenticationController(
            IAuthenticationProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignInAsync([FromBody]SignInModel signInModel)
        {
            var authUser = await _authenticationProvider.SignInAsync(signInModel);

            if (authUser == null)
            {
                return Unauthorized();
            }

            return Ok(authUser);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUpAsync([FromBody]SignUpModel model)
        {
            AuthUser authUser = await _authenticationProvider.SignUpAsync(model);

            if (authUser == null)
            {
                return Unauthorized();
            }

            return Ok(authUser);
        }

        [HttpPost]
        [Route("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            var r = Request;
            await _authenticationProvider.SignOutAsync();

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("GiveAdminRole")]
        public async Task<IActionResult> GiveAdminRole([FromQuery]string userId)
        {
            await _authenticationProvider.GiveAdminRole(userId);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("CreateRole")]
        public async Task<IActionResult> CreateRole([FromQuery]string name)
        {
            await _authenticationProvider.CreateRole(name);

            return Ok();
        }
    }

}