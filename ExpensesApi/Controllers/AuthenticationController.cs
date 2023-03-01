using Application.Authentication.IServices;
using Application.Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("/Login")]
        public ActionResult SignIn(AuthenticationRequest model)
        {
            try
            {
                var response = _authenticationService.SignIn(model);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/Register")]
        public ActionResult Register(RegistrationRequest model)
        {
            if(model.Password != model.ConfirmedPassword)
                return BadRequest("Passwords must be the same.");

            try
            {
                var response = _authenticationService.Register(model);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
