using CRM.Data.Abstraction;
using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(MainDatabaseContext context) : base(context)
        {
        }

        public async Task<Client> GetClientById(int id)
        {
            Client client = await _context.Clients
                .Include(x => x.ClientEmails)
                .Include(x => x.ClientPhoneNumbers)
                .FirstOrDefaultAsync(x => x.Id == id);

            return client;
        }

        public async Task<bool> CheckIfClientExistsByNip(string nip)
        {
            bool isClientExists = await _context.Clients.AnyAsync(x => x.Nip == nip);

            return isClientExists;
        }
    }
}
