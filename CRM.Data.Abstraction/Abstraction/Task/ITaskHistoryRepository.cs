using CRM.Infrastructure.Domain;

namespace CRM.Data.Abstraction
{
    public interface ITaskHistoryRepository : IGenericRepository<UserTaskHistory>
    {
        Task<List<UserTaskHistory>> GetTaskHistory(Guid taskId);
    }
}
