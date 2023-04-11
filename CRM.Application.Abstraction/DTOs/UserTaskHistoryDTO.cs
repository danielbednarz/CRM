namespace CRM.Application.Abstraction
{
    public class UserTaskHistoryDTO
    {
        public Guid Id { get; set; }
        public string FromStep { get; set; }
        public string ToStep { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
