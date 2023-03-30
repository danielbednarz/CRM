using CRM.Infrastructure.Domain;

namespace CRM.Application.Abstraction
{
    public interface ITaskService
    {
        Task<string> AddTask(UserTask userTask);
        Task<List<UserTaskDTO>> GetAllTasks();
        Task<UserTaskDTO> GetTaskDetails(Guid taskId);
        Task MoveToNextStep(Guid taskId, bool requireConfirmation);
        Task MoveToPreviousStep(Guid taskId, bool requireConfirmation);
        Task CancelTask(Guid taskId);
    }
}
