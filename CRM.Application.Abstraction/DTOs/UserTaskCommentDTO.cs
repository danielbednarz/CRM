namespace CRM.Application.Abstraction
{
    public class UserTaskCommentDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }
        public string CreatedBy { get; set; }
    }
}
