using CRM.Infrastructure.Domain;

namespace CRM.Data.Abstraction
{
    public interface ITaskCommentRepository : IGenericRepository<UserTaskComment>
    {
        Task<List<UserTaskComment>> GetComments(Guid taskId);
    }
}
