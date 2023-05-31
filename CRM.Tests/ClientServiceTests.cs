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
    public class ClientServiceTests 
    {
        private readonly ClientService _clientService;
        private readonly Mock<IClientRepository> _clientRepositoryMock = new();
        private readonly Mock<IClientNotesService> _clientNotesService = new();
        private readonly Mock<IUserService> _userService = new();

        public ClientServiceTests()
        {
            _clientService = new ClientService(_clientRepositoryMock.Object, _clientNotesService.Object, _userService.Object);
        }

        [Fact]
        public async Task GetClient_ReturnsClient_WhenClientExists()
        {
            // Arrange
            int clientId = 1;
            Client expectedClient = new()
            {
                Id = 1,
            };
            _clientRepositoryMock.Setup(repo => repo.GetClientById(clientId))
                .ReturnsAsync(expectedClient);
            // Act
            var client = await _clientService.GetClientById(clientId);
            // Assert
            Assert.NotNull(client);
            Assert.Equal(expectedClient.Id, client.Id);
        }

        [Fact]
        public async Task EditClient_ValidClient_EditsClient()
        {
            // Arrange
            var client = new Client
            {
                Id = 1,
                Name = "John Doe",
                Nip = "123456789",
                Krs = "987654321",
                Regon = "987654321",
                Country = "USA",
                City = "New York",
                IsActive = true,
                Rating = 5,
                Street = "Main Street"
            };

            _clientRepositoryMock.Setup(repo => repo.GetById(client.Id))
                .Returns(client);

            // Act
            await _clientService.EditClient(client);

            // Assert
            _clientRepositoryMock.Verify(repo => repo.SaveAsync(), Times.Once);
            var updatedClient = _clientRepositoryMock.Object.GetById(client.Id);

            Assert.Equal(client.Name, updatedClient.Name);
            Assert.Equal(client.Nip, updatedClient.Nip);
            Assert.Equal(client.Krs, updatedClient.Krs);
            Assert.Equal(client.Regon, updatedClient.Regon);
            Assert.Equal(client.Country, updatedClient.Country);
            Assert.Equal(client.City, updatedClient.City);
            Assert.Equal(client.IsActive, updatedClient.IsActive);
            Assert.Equal(client.Rating, updatedClient.Rating);
            Assert.Equal(client.Street, updatedClient.Street);
        }

        [Fact]
        public void DeleteClient_ValidId_RemovesClientAndReturnsTrue()
        {
            // Arrange
            int clientId = 1;
            var clientToDelete = new Client { Id = clientId, Name = "John Doe" };

            _clientRepositoryMock.Setup(repo => repo.GetById(clientId))
                .Returns(clientToDelete);

            // Act
            var result = _clientService.DeleteClient(clientId);

            // Assert
            _clientRepositoryMock.Verify(repo => repo.Remove(clientToDelete), Times.Once);
            Assert.True(result);
        }

    }
}