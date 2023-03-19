namespace CRM.Application.Abstraction
{
    public class AddClientDocumentVM
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public int ClientId { get; set; }
    }
}
