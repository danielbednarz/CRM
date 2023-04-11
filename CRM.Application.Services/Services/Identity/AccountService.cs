using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRM.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }


        public async Task<UserDTO> Login(LoginVM model)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == model.Email.ToLower());

            if (user == null)
            {
                return null;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                return null;
            }

            return new UserDTO
            {
                Username = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }
    }
}
