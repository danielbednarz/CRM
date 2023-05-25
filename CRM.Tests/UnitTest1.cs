using CRM.Application.Abstraction;
using CRM.Application.Services;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using CRM.WebAPI;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using MimeKit.Utils;
using Moq;

namespace CRM.Tests
{
    public class UnitTest1 
    {

        private readonly ClientService _clientService;
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();
        private readonly Mock<IClientNotesService> _clientNotesService = new();
        private readonly Mock<IUserService> _userService = new();

        public UnitTest1()
        {
            _clientService = new ClientService(_clientRepositoryMock.Object, _clientNotesService.Object, _userService.Object);
        }

        [Fact]
        public async Task GetClient_ShouldReturnClient_WhenClientExists()
        {
            var clientId = 1;

            var client = await _clientService.GetClientById(clientId);

            Assert.NotNull(client);
            
        }


    }
}