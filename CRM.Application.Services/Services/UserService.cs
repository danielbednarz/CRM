using CRM.Application.Abstraction;
using CRM.Application.Services.Converters;
using CRM.Data.Abstraction;

namespace CRM.Application.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; set; }

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<AppUserVM>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();

            List<AppUserVM> userVMs = new();

            foreach (var user in users)
            {
                userVMs.Add(UserConverter.ToAppUserVM(user));
            }

            return userVMs;
        }


    }
}
