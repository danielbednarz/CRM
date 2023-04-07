using CRM.Data.Abstraction;
using CRM.EntityFramework.Context;
using CRM.Infrastructure.Domain;
using Microsoft.EntityFrameworkCore;

namespace CRM.Data.Repositories
{
    public class TaskCommentRepository : GenericRepository<UserTaskComment>, ITaskCommentRepository
    {
        public TaskCommentRepository(MainDatabaseContext context) : base(context)
        {
        }

        public async Task<List<UserTaskComment>> GetComments(Guid taskId)
        {
            return await _context.UserTaskComments.Where(x => x.UserTaskId == taskId).ToListAsync();
        }
    }
}
