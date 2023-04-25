using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace CRM.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepository;

        public RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IUserRepository userRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<List<RoleDTO>> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<RoleDTO> roleDTOs = new();

            roles.ForEach(role =>
            {
                roleDTOs.Add(new RoleDTO()
                {
                    Id = role.Id,
                    Name = role.Name,
                });
            });

            return roleDTOs;
        }

        public async Task<RoleDetailsDTO> GetRoleDetails(int id)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);

            var users = role.UserRoles.Where(x => x.RoleId == id).Select(y => y.User).ToList();
            List<UserSelectDTO> roleUsers = new();

            users.ForEach(user =>
            {
                roleUsers.Add(new UserSelectDTO()
                {
                    Id = user.Id,
                    Name = user.FirstName + " " + user.LastName
                });
            });

            RoleDetailsDTO roleDetails = new()
            {
                Id = role.Id,
                Name = role.Name,
                Users = roleUsers
            };

            return roleDetails;
        }

        public async Task DeleteUserFromRole(int userId, int roleId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new Exception("Cannot find user with given id");
            }

            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            if (role == null)
            {
                throw new Exception("Cannot find role with given id");
            }

            var isInRole = await _userManager.IsInRoleAsync(user, role.Name);

            if (!isInRole)
            {
                throw new Exception("User is not in role");
            }

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);

            if (!result.Succeeded)
            {
                throw new Exception("Cannot remove user from role");
            }
        }

        public async Task<List<UserSelectDTO>> GetUsersAvailableToAdd(int roleId)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            if (role == null)
            {
                throw new Exception("Cannot find role with given id");
            }

            var usersNotInRole = await _userRepository.GetUsersNotInRole(roleId);
            List<UserSelectDTO> usersToSelect = new();

            usersNotInRole.ForEach(users =>
            {
                usersToSelect.Add(new UserSelectDTO
                {
                    Id = users.Id,
                    Name = users.FirstName + " " + users.LastName
                });
            });

            return usersToSelect;
        }

        public async Task AddUsersToRole(int roleId, List<int> userIds)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);

            if (role == null)
            {
                throw new Exception("Cannot find role with given id");
            }

            var users = await _userManager.Users
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();

            if (users.Count != userIds.Count)
            {
                throw new Exception("Cannot find some users");
            }

            foreach (var user in users)
            {
                if (!await _userManager.IsInRoleAsync(user, role.Name))
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

        }
    }
}
