using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
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
    }
}
