using CRM.Infrastructure.Dictionaries;

namespace CRM.Application.Abstraction
{
    public interface ITaskHistoryService
    {
        Task AddStepHistory(Guid taskId, UserTaskStepType? fromStep, UserTaskStepType toStep);
        Task<List<UserTaskHistoryDTO>> GetTaskHistory(Guid taskId);
    }
}
