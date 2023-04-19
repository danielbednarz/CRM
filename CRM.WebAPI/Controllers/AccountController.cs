using CRM.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI
{
    public class AccountController : AppController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                await _accountService.Register(model);

                return Ok();
            }

            return BadRequest("Error when trying to create an account");
        }

        //[HttpGet("confirmEmail")]
        //public async Task<ActionResult> ConfirmEmail(int id, string token)
        //{
        //    await _accountService.ConfirmEmail(id, token);

        //    return Ok();
        //}

        [HttpPost("confirmEmail")]
        public async Task<ActionResult> ConfirmEmail(ConfirmRegisterVM model)
        {
            await _accountService.ConfirmEmail(model.UserId, model.Token, model.Password);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.Login(model);

                if (user == null)
                {
                    return Unauthorized("Wrong login or password");
                }

                return user;
            }

            return BadRequest("Error when trying to login");
        }

    }
}
