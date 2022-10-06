using CRM.Infrastructure.Domain;

namespace CRM.Application.Abstraction
{
    public interface IClientService
    {
        Task<int> AddClient(Client client);
        Task<bool> CheckIsClientExistsByNip(string nip);
        Task<List<Client>> GetClients();
        Task<Client> GetClientById(int id);
        Task<Client> GetClientByNip(string nip);
        bool DeleteClient(int id);
    }
}
