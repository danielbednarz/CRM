using CRM.Application.Abstraction;
using CRM.Application.Services;
using CRM.Infrastructure.Domain;
using CRM.WebAPI;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CRM.Tests
{
    public class UnitTest1 
    {

        private readonly IAccountService _accountService;

        public UnitTest1(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Fact]
        public async void ShouldAuthenticateValidUser()
        {
            var result = await _accountService.Login(new LoginVM { Email = "test@test.pl", Password = "Start.123" });

            Assert.NotNull(result);
        }

        [Fact]
        public async void ShouldNotAuthenticateValidUser()
        {
            var result = await _accountService.Login(new LoginVM { Email = "test@test.pl", Password = "Start235" });

            Assert.Null(result);
        }

    }
}