using AutoMapper.Configuration.Conventions;
using CRM.Application.Abstraction;
using CRM.Application.Services.Converters;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CRM.Application.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<AppUserVM>> GetUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            List<AppUserVM> userVMs = new();

            foreach (var user in users)
            {
                userVMs.Add(UserConverter.ToAppUserVM(user));
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
    }
}
