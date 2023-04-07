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
        private readonly ITaskCommentService _taskCommentService;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper, IUserRepository userRepository, IClientRepository clientRepository, ITaskCommentService taskCommentRepository)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _taskCommentService = taskCommentRepository;
        }

        public async Task<Guid> AddTask(UserTask userTask)
        {
            userTask.Signature = await GenerateSignature();
            userTask.Step = UserTaskStepType.Start;

            _taskRepository.Add(userTask);
            await _taskRepository.SaveAsync();

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

            var comments = await _taskCommentService.GetComments(task.Id);
            List<UserTaskCommentDTO> commentsDTO = new();

            foreach(var comment in comments)
            {
                UserTaskCommentDTO commentDTO = _mapper.Map<UserTaskCommentDTO>(comment);

                commentsDTO.Add(commentDTO);
            }

            UserTaskDTO taskDTO = _mapper.Map<UserTaskDTO>(task);
            taskDTO.Step = EnumExtensions.GetEnumDisplayName(task.Step);
            taskDTO.StepValue = (int)task.Step;
            taskDTO.Priority = EnumExtensions.GetEnumDisplayName(task.Priority);
            taskDTO.AssignedUser = await _userRepository.GetUserNameSurnameString(task.AssignedUserId);
            taskDTO.Supervisor = await _userRepository.GetUserNameSurnameString(task.SupervisorId);
            taskDTO.ClientName = await _clientRepository.GetClientNameString(task.ClientId);
            taskDTO.Comments = commentsDTO;

            return taskDTO;
        }

        public async Task MoveToNextStep(Guid taskId)
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
        }

        public async Task MoveToPreviousStep(Guid taskId)
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

            if (task.Step == UserTaskStepType.End && !task.RequireConfirmation)
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
