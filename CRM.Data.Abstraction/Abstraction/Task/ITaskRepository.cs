using CRM.Infrastructure.Domain;

namespace CRM.Data.Abstraction
{
    public interface ITaskRepository : IGenericRepository<UserTask>
    {
        Task<int> GetCountForCurrentYear();
    }
}
