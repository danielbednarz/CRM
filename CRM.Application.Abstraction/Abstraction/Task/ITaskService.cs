using CRM.Infrastructure.Domain;

namespace CRM.Application.Abstraction
{
    public interface ITaskService
    {
        Task<Guid> AddTask(UserTask userTask);
        Task<List<UserTaskDTO>> GetAllTasks();
        Task<UserTaskDTO> GetTaskDetails(Guid taskId);
        Task<int> GetUserTasksCount(int userId);
        Task MoveToNextStep(Guid taskId);
        Task MoveToPreviousStep(Guid taskId);
        Task CancelTask(Guid taskId);
    }
}
