using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Dictionaries;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper, IUserRepository userRepository, IClientRepository clientRepository)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
        }

        public async Task<string> AddTask(UserTask userTask)
        {
            userTask.Signature = await GenerateSignature();
            userTask.Step = UserTaskStepType.Start;

            _taskRepository.Add(userTask);
            await _taskRepository.SaveAsync();

            return userTask.Signature;
        }

        public async Task<List<UserTaskDTO>> GetAllTasks()
        {
            List<UserTask> tasks = await _taskRepository.GetAllAsync();
            List<UserTaskDTO> result = new();

            foreach (UserTask task in tasks)
            {
                UserTaskDTO taskDTO = _mapper.Map<UserTaskDTO>(task);
                taskDTO.Step = EnumExtensions.GetEnumDisplayName(task.Step);
                taskDTO.Priority = EnumExtensions.GetEnumDisplayName(task.Priority);
                taskDTO.AssignedUser = await _userRepository.GetUserNameSurnameString(task.AssignedUserId);
                taskDTO.Supervisor = await _userRepository.GetUserNameSurnameString(task.SupervisorId);
                taskDTO.ClientName = await _clientRepository.GetClientNameString(task.ClientId);

                result.Add(taskDTO);
            }

            return result;
        }

        public async Task<UserTaskDTO> GetTaskDetails(Guid taskId)
        {
            UserTask task = await _taskRepository.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                throw new Exception("Cannot find task with given ID");
            }

            UserTaskDTO result = _mapper.Map<UserTaskDTO>(task);
            result.Step = EnumExtensions.GetEnumDisplayName(task.Step);
            result.Priority = EnumExtensions.GetEnumDisplayName(task.Priority);

            return result;
        }

        public async Task MoveToNextStep(Guid taskId, bool requireConfirmation)
        {
            UserTask task = await _taskRepository.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                throw new Exception("Cannot find task with given ID");
            }

            if (task.Step == UserTaskStepType.End || task.Step == UserTaskStepType.Cancel)
            {
                throw new Exception("Task is on last step");
            }

            if (task.Step == UserTaskStepType.Middle)
            {
                if (requireConfirmation)
                {
                    task.Step = UserTaskStepType.Confirmation;
                }
                else
                {
                    task.Step = UserTaskStepType.End;
                }
            }
            else
            {
                task.Step = (UserTaskStepType)(((int)task.Step) + 1);
            }

            await _taskRepository.SaveAsync();
        }

        public async Task MoveToPreviousStep(Guid taskId, bool requireConfirmation)
        {
            UserTask task = await _taskRepository.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                throw new Exception("Cannot find task with given ID");
            }

            if (task.Step == UserTaskStepType.Start)
            {
                throw new Exception("Task is on first step");
            }

            if (task.Step == UserTaskStepType.End && !requireConfirmation)
            {
                    task.Step = UserTaskStepType.Middle;
            }
            else
            {
                task.Step = (UserTaskStepType)(((int)task.Step) - 1);
            }

            await _taskRepository.SaveAsync();
        }

        private async Task<string> GenerateSignature()
        {
            int count = await _taskRepository.GetCountForCurrentYear();
            DateTime date = DateTime.Now;

            return @$"Z{count + 1}/{date.Year}";
        }
    }
}
