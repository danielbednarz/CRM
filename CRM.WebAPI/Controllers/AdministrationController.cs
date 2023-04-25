using CRM.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI
{
    public class AdministrationController : AppController
    {
        private IUserService _userService { get; set; }
        private IRoleService _roleService { get; set; }

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

        [HttpGet("getRoleDetails")]
        public async Task<ActionResult> GetRoleDetails(int id)
        {
            var data = await _roleService.GetRoleDetails(id);

            return Ok(data);
        }

        [HttpGet("getUsersAvailableToAdd")]
        public async Task<ActionResult> GetUsersAvailableToAdd(int roleId)
        {
            var data = await _roleService.GetUsersAvailableToAdd(roleId);

            return Ok(data);
        }

        [HttpPost("addUsersToRole")]
        public async Task<IActionResult> AddUsersToRole(AddUsersToRoleVM model)
        {
            await _roleService.AddUsersToRole(model.RoleId, model.UserIds);

            return Ok();
        }

        [HttpDelete("deleteUserFromRole")]
        public async Task<ActionResult> DeleteUserFromRole(int userId, int roleId)
        {
            await _roleService.DeleteUserFromRole(userId, roleId);

            return Ok();
        }
    }
}
