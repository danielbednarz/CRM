using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int> AddClient(Client client)
        {
            _clientRepository.Add(client);
            await _clientRepository.SaveAsync();

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
