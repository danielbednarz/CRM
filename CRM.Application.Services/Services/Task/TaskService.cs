using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Dictionaries;
using CRM.Infrastructure.Domain;
using System.Threading.Tasks;

namespace CRM.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskCommentService _taskCommentService;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ITaskHistoryService _taskHistoryService;

        public TaskService(ITaskRepository taskRepository, IMapper mapper, IUserRepository userRepository, IClientRepository clientRepository, ITaskCommentService taskCommentRepository, ITaskHistoryService taskHistoryService)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _taskCommentService = taskCommentRepository;
            _taskHistoryService = taskHistoryService;
        }

        public async Task<Guid> AddTask(UserTask userTask)
        {
            userTask.Signature = await GenerateSignature();
            userTask.Step = UserTaskStepType.Start;

            _taskRepository.Add(userTask);
            await _taskRepository.SaveAsync();
            await _taskHistoryService.AddStepHistory(userTask.Id, null, userTask.Step);

            return userTask.Id;
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

            UserTaskDTO taskDTO = _mapper.Map<UserTaskDTO>(task);

            taskDTO.Step = EnumExtensions.GetEnumDisplayName(task.Step);
            taskDTO.StepValue = (int)task.Step;
            taskDTO.Priority = EnumExtensions.GetEnumDisplayName(task.Priority);
            taskDTO.AssignedUser = await _userRepository.GetUserNameSurnameString(task.AssignedUserId);
            taskDTO.Supervisor = await _userRepository.GetUserNameSurnameString(task.SupervisorId);
            taskDTO.ClientName = await _clientRepository.GetClientNameString(task.ClientId);
            taskDTO.Comments = await _taskCommentService.GetComments(task.Id);
            taskDTO.History = await _taskHistoryService.GetTaskHistory(task.Id);

            return taskDTO;
        }

        public async Task MoveToNextStep(Guid taskId)
        {
            UserTask task = await _taskRepository.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                throw new Exception("Cannot find task with given ID");
            }

            UserTaskStepType currentStep = task.Step;

            if (task.Step == UserTaskStepType.End || task.Step == UserTaskStepType.Cancel)
            {
                throw new Exception("Task is on last step");
            }

            if (task.Step == UserTaskStepType.Middle)
            {
                if (task.RequireConfirmation)
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
            await _taskHistoryService.AddStepHistory(task.Id, currentStep, task.Step);
        }

        public async Task MoveToPreviousStep(Guid taskId)
        {
            UserTask task = await _taskRepository.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                throw new Exception("Cannot find task with given ID");
            }

            UserTaskStepType currentStep = task.Step;

            if (task.Step == UserTaskStepType.Start)
            {
                throw new Exception("Task is on first step");
            }

            if (task.Step == UserTaskStepType.End && !task.RequireConfirmation)
            {
                task.Step = UserTaskStepType.Middle;
            }
            else
            {
                task.Step = (UserTaskStepType)(((int)task.Step) - 1);
            }

            await _taskRepository.SaveAsync();
            await _taskHistoryService.AddStepHistory(task.Id, currentStep, task.Step);
        }

        private async Task<string> GenerateSignature()
        {
            int count = await _taskRepository.GetCountForCurrentYear();
            DateTime date = DateTime.Now;

            return @$"Z{count + 1}/{date.Year}";
        }


        public async Task CancelTask(Guid taskId)
        {
            UserTask task = await _taskRepository.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                throw new Exception("Cannot find task with given ID");
            }

            task.Step = UserTaskStepType.Cancel;

            await _taskRepository.SaveAsync();
        }
    }
}
