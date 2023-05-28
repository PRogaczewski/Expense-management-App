using Application.Authentication.IServices;
using Application.Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPut("/ChangePassword")]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequest model)
        {
            if (model.NewPassword != model.ConfirmedNewPassword)
                return BadRequest("Passwords must be the same.");

            try
            {
                var result = await _accountService.ChangePassword(model);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/DeleteAccount")]
        public async Task<ActionResult> DeleteAccount()
        {
            try
            {
                var result = await _accountService.DeleteAccount();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
