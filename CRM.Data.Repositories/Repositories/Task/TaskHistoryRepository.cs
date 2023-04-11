using CRM.Data.Abstraction;
using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Repositories
{
    public class TaskHistoryRepository : GenericRepository<UserTaskHistory>, ITaskHistoryRepository
    {
        public TaskHistoryRepository(MainDatabaseContext context) : base(context)
        {
        }

        public async Task<List<UserTaskHistory>> GetTaskHistory(Guid taskId)
        {
            return await _context.UserTaskHistories.Where(x => x.UserTaskId == taskId).ToListAsync();
        }
    }
}
