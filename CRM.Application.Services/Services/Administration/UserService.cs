using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Application.Services.Converters;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, UserManager<AppUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UserDetailsDTO>> GetUsersAsync()
        {
            List<AppUser> users = await _userRepository.GetAllAsync();

            List<UserDetailsDTO> userDTOs = new();

            foreach (var user in users)
            {
                UserDetailsDTO userDTO = new()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IsActive = user.IsActive,
                    Roles = await GetUserRoles(user.Id)
                };

                userDTOs.Add(userDTO);
            }

            return userDTOs;
        }

        public async Task<AppUser> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<UserDetailsDTO> GetUserDetails(int id)
        {
            AppUser user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            UserDetailsDTO userDTO = new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                Roles = await GetUserRoles(id)
            };

            return userDTO;
        }

        public async Task<AppUser> GetAdminAsync()
        {
            return await _userRepository.FirstOrDefaultAsync(x => x.UserName.Contains("admin"));
        }

        private async Task<List<RoleDTO>> GetUserRoles(int id)
        {
            List<AppRole> userRoles = await _userRepository.GetUserRoles(id);
            List<RoleDTO> roleDTOs = new();
            userRoles.ForEach(role =>
            {
                roleDTOs.Add(new RoleDTO
                {
                    Id = role.Id,
                    Name = role.Name
                });
            });

            return roleDTOs;
        }

        public async Task EditUser(EditUserVM model)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (user == null)
            {
                throw new Exception("There is no user with given id");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            user.IsActive = model.IsActive;

            await _userManager.UpdateAsync(user);

        }

        public async Task<List<UserSelectDTO>> GetUsersToSelect()
        {
            List<AppUser> users = await _userRepository.GetAllAsync();

            List<UserSelectDTO> userSelectDTOs = new();

            users.Where(x => x.IsActive).ToList().ForEach(users =>
            {
                UserSelectDTO userDTO = new()
                {
                    Id = users.Id,
                    Name = users.FirstName + " " + users.LastName,
                };
                userSelectDTOs.Add(userDTO);
            });

            return userSelectDTOs;
        }
    }
}
