using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI
{
    public class TasksController : AppController
    {
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;

        public TasksController(IMapper mapper, ITaskService taskService)
        {
            _mapper = mapper;
            _taskService = taskService;
        }


        [HttpPost("addTask")]
        public async Task<ActionResult> AddTask(AddUserTaskVM model)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be null");
            }

            UserTask taskToAdd = _mapper.Map<UserTask>(model);

            string signature = await _taskService.AddTask(taskToAdd);

            return Ok(signature);
        }

        [HttpGet("getAllTasks")]
        public async Task<ActionResult> GetAllTasks()
        {
            List<UserTaskDTO> result = await _taskService.GetAllTasks();

            return Ok(result);
        }

        [HttpGet("getTaskDetails")]
        public async Task<ActionResult> GetTaskDetails(Guid taskId)
        {
            UserTaskDTO result = await _taskService.GetTaskDetails(taskId);

            return Ok(result);
        }

        [HttpPost("moveToNextStep")]
        public async Task<ActionResult> MoveToNextStep(UserTaskSwitchStepVM model)
        {
            await _taskService.MoveToNextStep(model.TaskId, model.RequireConfirmation);

            return Ok();
        }

        [HttpPost("moveToPreviousStep")]
        public async Task<ActionResult> MoveToPreviousStep(UserTaskSwitchStepVM model)
        {
            await _taskService.MoveToPreviousStep(model.TaskId, model.RequireConfirmation);

            return Ok();
        }
    }
}
