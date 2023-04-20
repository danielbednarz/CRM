using CRM.Data.Abstraction;
using CRM.EntityFramework.Context;
using CRM.Infrastructure.Dictionaries;
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

        public async Task<int> GetUserTasksCount(int userId)
        {
            return await _context.UserTasks.CountAsync(x => (x.AssignedUserId == userId || x.SupervisorId == userId) && x.Step != UserTaskStepType.End && x.Step != UserTaskStepType.Cancel);
        }
    }
}
