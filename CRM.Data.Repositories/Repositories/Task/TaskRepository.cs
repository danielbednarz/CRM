using CRM.Data.Abstraction;
using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Repositories
{
    public class TaskRepository : GenericRepository<UserTask>, ITaskRepository
    {
        public TaskRepository(MainDatabaseContext context) : base(context)
        {
        }

        public async Task<int> GetCountForCurrentYear()
        {
            int currentYear = DateTime.Now.Year;

            return await _context.UserTasks.CountAsync(x => x.CreatedDate.Year == currentYear);
        }
    }
}
