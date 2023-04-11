using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _taskCommentRepository;
        private readonly IUserRepository _userRepository;

        public TaskCommentService(ITaskCommentRepository taskCommentRepository, IUserRepository userRepository)
        {
            _taskCommentRepository = taskCommentRepository;
            _userRepository = userRepository;
        }

        public async Task AddComment(UserTaskComment comment)
        {
            _taskCommentRepository.Add(comment);
            await _taskCommentRepository.SaveAsync();
        }

        public async Task<List<UserTaskCommentDTO>> GetComments(Guid taskId)
        {
            List<UserTaskComment> comments = await _taskCommentRepository.GetComments(taskId);
            List<UserTaskCommentDTO> commentsDTO = new();

            foreach (var comment in comments)
            {
                UserTaskCommentDTO commentDTO = new()
                {
                    Id = comment.Id,
                    CreatedById = comment.CreatedById,
                    Content = comment.Content,
                    CreatedDate = comment.CreatedDate,
                    CreatedBy = await _userRepository.GetUserNameSurnameString(comment.CreatedById)
                };

                commentsDTO.Add(commentDTO);
            }

            return commentsDTO;
        }

        public void DeleteComment(Guid commentId)
        {
            UserTaskComment commentToDelete = _taskCommentRepository.FirstOrDefault(x => x.Id == commentId);
            if (commentToDelete == null)
            {
                throw new Exception("Cannot find note");
            }

            _taskCommentRepository.Remove(commentToDelete);
            _taskCommentRepository.Save();
        }
    }
}
