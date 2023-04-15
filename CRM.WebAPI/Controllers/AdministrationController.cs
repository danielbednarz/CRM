using CRM.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI
{
    public class AdministrationController : AppController
    {

        public IUserService _userService { get; set; }

        public AdministrationController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet("getUsers")]
        public async Task<ActionResult> GetUsers()
        {
            var data = await _userService.GetUsersAsync();

            return Ok(data);
        }

        [HttpGet("getRoles")]
        public async Task<ActionResult> GetRoles()
        {

        }
    }
}
