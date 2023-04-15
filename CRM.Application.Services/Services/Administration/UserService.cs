using AutoMapper.Configuration.Conventions;
using CRM.Application.Abstraction;
using CRM.Application.Services.Converters;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRM.Application.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; set; }
        private readonly UserManager<AppUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<List<AppUserVM>> GetUsersAsync()
        {
            List<AppUser> users = await _userRepository.GetAllAsync();

            List<AppUserVM> userVMs = new();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userVMs.Add(UserConverter.ToAppUserVM(user, roles));
            }

            return userVMs;
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<AppUser> GetAdminAsync()
        {
            return await _userRepository.FirstOrDefaultAsync(x => x.UserName.Contains("admin"));
        }

        private List<string> GetUserRoles(ClaimsIdentity identity)
        {
            return identity.Claims
                           .Where(c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList();
        }
    }
}
