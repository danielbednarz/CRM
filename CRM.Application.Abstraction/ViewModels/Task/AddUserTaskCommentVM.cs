namespace CRM.Application.Abstraction
{
    public class AddUserTaskCommentVM
    {
        public Guid UserTaskId { get; set; }
        public string Content { get; set; }
    }
}
