using CRM.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI
{
    public class AdministrationController : AppController
    {

        public IUserService _userService { get; set; }
        public IRoleService _roleService { get; set; }

        public AdministrationController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }


        [HttpGet("getUsers")]
        public async Task<ActionResult> GetUsers()
        {
            var data = await _userService.GetUsersAsync();

            return Ok(data);
        }

        [HttpGet("getUserDetails")]
        public async Task<ActionResult> GetUserDetails(int id)
        {
            var data = await _userService.GetUserDetails(id);

            return Ok(data);
        }

        [HttpPut("editUser")]
        public async Task<ActionResult> EditUser(EditUserVM model)
        {
            await _userService.EditUser(model);

            return Ok();
        }

        [HttpGet("getRoles")]
        public async Task<ActionResult> GetRoles()
        {
            var data = await _roleService.GetRoles();

            return Ok(data);
        }
    }
}
