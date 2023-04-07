using CRM.Infrastructure.Domain;

namespace CRM.Application.Abstraction
{
    public interface ITaskCommentService
    {
        Task AddComment(UserTaskComment comment);
        Task<List<UserTaskCommentDTO>> GetComments(Guid taskId);
    }
}
