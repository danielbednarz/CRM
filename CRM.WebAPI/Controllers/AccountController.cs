using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI
{
    public class AccountController : AppController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper, IAccountService accountService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<AppUser>(model);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }

                return new UserDTO
                {
                    Username = user.UserName,
                    Token = await _tokenService.CreateToken(user)
                };
            }

            return BadRequest("Error when trying to create an account");
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await _accountService.Login(model);

                if (user == null)
                {
                    return Unauthorized("Wrong login or password");
                }

                return user;
            }

            return BadRequest("Error when trying to login");
        }

    }
}
