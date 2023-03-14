using CRM.Data.Abstraction;
using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Repositories
{
    public class ClientNotesRepository : GenericRepository<ClientNote>, IClientNotesRepository
    {
        public ClientNotesRepository(MainDatabaseContext context) : base(context)
        {
        }

        public Task<List<ClientNote>> GetClientNotes(int clientId)
        {
            return _context.ClientNotes.Where(x => x.ClientId == clientId).ToListAsync();
        }
    }
}
