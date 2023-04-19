namespace CRM.Application.Abstraction
{
    public class ConfirmRegisterVM
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
