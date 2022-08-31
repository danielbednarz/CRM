using CRM.Application.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CRM.WebAPI
{
    public class UsersController : AppController
    {

        public IUserService _userService { get; set; }

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("checkConnection")]
        public ActionResult CheckConnection()
        {  
            return Ok();
        }

        [HttpGet("getUsers")]
        public async Task<ActionResult> GetUsers()
        {
            var data = await _userService.GetUsers();

            return Ok(data);
        }
    }
}
