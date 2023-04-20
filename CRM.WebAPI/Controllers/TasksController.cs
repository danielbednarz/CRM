using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Application.Services;
using CRM.Infrastructure.Dictionaries;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM.WebAPI
{
    [Authorize]
    public class TasksController : AppController
    {
        private readonly IMapper _mapper;
        private readonly ITaskService _taskService;
        private readonly ITaskCommentService _taskCommentService;
        private readonly IUserService _userService;

        public TasksController(IMapper mapper, ITaskService taskService, IUserService userService, ITaskCommentService taskCommentService)
        {
            _mapper = mapper;
            _taskService = taskService;
            _userService = userService;
            _taskCommentService = taskCommentService;
        }

        [Authorize(Roles = AppRoleType.Admin)]
        [HttpPost("addTask")]
        public async Task<ActionResult> AddTask(AddUserTaskVM model)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be null");
            }

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUserByUsernameAsync(username);

            var taskToAdd = _mapper.Map<UserTask>(model);
            taskToAdd.CreatedById = user.Id;

            var id = await _taskService.AddTask(taskToAdd);

            return Ok(id);
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

        [HttpGet("getUserTasksCount")]
        public async Task<ActionResult> GetUserTasksCount()
        {
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUserByUsernameAsync(username);

            int result = await _taskService.GetUserTasksCount(user.Id);

            return Ok(result);
        }

        [HttpPost("moveToNextStep")]
        public async Task<ActionResult> MoveToNextStep([FromQuery]Guid taskId)
        {
            await _taskService.MoveToNextStep(taskId);

            return Ok();
        }

        [HttpPost("moveToPreviousStep")]
        public async Task<ActionResult> MoveToPreviousStep([FromQuery]Guid taskId)
        {
            await _taskService.MoveToPreviousStep(taskId);

            return Ok();
        }

        [HttpPost("cancelTask")]
        public async Task<ActionResult> CancelTask([FromQuery]Guid taskId)
        {
            await _taskService.CancelTask(taskId);

            return Ok();
        }

        [HttpGet("getUsersToSelect")]
        public async Task<ActionResult> GetUsersToSelect()
        {
            var users = await _userService.GetUsersToSelect();

            return Ok(users);
        }

        #region Comments

        [HttpPost("addComment")]
        public async Task<ActionResult> AddComment(AddUserTaskCommentVM model)
        {
            if (model == null)
            {
                return BadRequest("Model cannot be null");
            }

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUserByUsernameAsync(username);

            var commentToAdd = _mapper.Map<UserTaskComment>(model);
            commentToAdd.CreatedById = user.Id;

            await _taskCommentService.AddComment(commentToAdd);

            return Ok();
        }

        [HttpDelete("deleteComment")]
        public ActionResult DeleteComment(Guid commentId)
        {
            _taskCommentService.DeleteComment(commentId);

            return Ok();
        }

        #endregion
    }
}
