using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using System.Security.Claims;

namespace CRM.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IClientNotesService _clientNotesService;
        private readonly IUserService _userService;

        public ClientService(IClientRepository clientRepository, IClientNotesService clientNotesService, IUserService userService)
        {
            _clientRepository = clientRepository;
            _clientNotesService = clientNotesService;
            _userService = userService;
        }

        public async Task<int> AddClient(Client client)
        {
            _clientRepository.Add(client);
            await _clientRepository.SaveAsync();

            AppUser admin = await _userService.GetAdminAsync();

            await _clientNotesService.AddNote(new ClientNote
            {
                Title = "Dodanie klienta",
                Content = "Dodano klienta do systemu",
                UserId = admin.Id,
                ClientId = client.Id
            });

            var clientId = client.Id;

            return clientId;
        }

        public async Task EditClient(Client client)
        {
            var clientToEdit = _clientRepository.GetById(client.Id);

            clientToEdit.Name = client.Name;
            clientToEdit.Nip = client.Nip;
            clientToEdit.Krs = client.Krs;
            clientToEdit.Regon = client.Regon;
            clientToEdit.Country = client.Country;
            clientToEdit.City= client.City;
            clientToEdit.BuildingNumber = client.BuildingNumber;
            clientToEdit.IsActive = client.IsActive;
            clientToEdit.Rating = client.Rating;

            await _clientRepository.SaveAsync();
        }

        public async Task<bool> CheckIsClientExistsByNip(string nip)
        {
            bool isClientExists = await _clientRepository.CheckIfClientExistsByNip(nip);

            return isClientExists;
        }

        public bool DeleteClient(int id)
        {
            var clientToDelete = _clientRepository.GetById(id);

            if (clientToDelete == null)
            {
                return false;
            }

            _clientRepository.Remove(clientToDelete);

            return true;
        }

        public async Task<Client> GetClientById(int id)
        {
            var client = await _clientRepository.GetClientById(id);

            return client;
        }

        public async Task<Client> GetClientByNip(string nip)
        {
            var client = await _clientRepository.FirstOrDefaultAsync(x => x.Nip == nip);

            return client;
        }

        public Task<List<Client>> GetClients()
        {
            var clients = _clientRepository.GetAllAsync();

            return clients;
        }
    }
}
