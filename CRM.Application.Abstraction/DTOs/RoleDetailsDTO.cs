namespace CRM.Application.Abstraction
{
    public class RoleDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserSelectDTO> Users { get; set; }
    }
}
