using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Dictionaries;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CRM.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IEmailSenderService _emailSender;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AccountService(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper, IEmailSenderService emailSender, IUserRepository userRepository, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _emailSender = emailSender;
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task Register(RegisterVM model)
        {
            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, _configuration["Identity:Secret"]);

            if (!result.Succeeded)
            {
                throw new Exception("Cannot create user");
            }
            
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailSender.SendConfirmationEmail(user, token);
        }

        public async Task ConfirmEmail(int id, string token, string password)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new Exception("Cannot find user with given email");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                throw new Exception("Cannot confirm email");
            }
            var pwdResult = await _userManager.ChangePasswordAsync(user, _configuration["Identity:Secret"], password);
            if (!pwdResult.Succeeded)
            {
                throw new Exception("Cannot set password");
            }

            user.IsActive = true;
            await _userRepository.SaveAsync();
            await _userManager.AddToRoleAsync(user, AppRoleType.User);
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
