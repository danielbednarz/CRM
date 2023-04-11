using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Dictionaries;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Services
{
    public class TaskHistoryService : ITaskHistoryService
    {
        private readonly ITaskHistoryRepository _taskHistoryRepository;

        public TaskHistoryService(ITaskHistoryRepository taskHistoryRepository)
        {
            _taskHistoryRepository = taskHistoryRepository;
        }

        public async Task AddStepHistory(Guid taskId, UserTaskStepType? fromStep, UserTaskStepType toStep)
        {
            UserTaskHistory taskHistory = new()
            {
                UserTaskId = taskId,
                FromStep = fromStep,
                ToStep = toStep
            };
            _taskHistoryRepository.Add(taskHistory);
            await _taskHistoryRepository.SaveAsync();
        }

        public async Task<List<UserTaskHistoryDTO>> GetTaskHistory(Guid taskId)
        {
            List<UserTaskHistory> taskHistory = await _taskHistoryRepository.GetTaskHistory(taskId);
            List<UserTaskHistoryDTO> result = new();

            taskHistory.ForEach(taskHistoryItem => 
            {
                result.Add(new UserTaskHistoryDTO
                {
                    Id = taskHistoryItem.Id,
                    CreatedDate= taskHistoryItem.CreatedDate,
                    FromStep = EnumExtensions.GetEnumDisplayName(taskHistoryItem.FromStep),
                    ToStep = EnumExtensions.GetEnumDisplayName(taskHistoryItem.ToStep),
                });
            });

            return result;
        }
    }
}
