using CRM.Data.Abstraction;
using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Repositories
{
    public class ClientDocumentRepository : GenericRepository<ClientDocument>, IClientDocumentRepository
    {
        public ClientDocumentRepository(MainDatabaseContext context) : base(context)
        {
        }

        public async Task<List<ClientDocument>> GetClientDocuments(int clientId)
        {
            return _context.ClientDocuments.Where(x => x.ClientId == clientId).ToList();
        }
    }
}
