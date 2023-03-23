namespace CRM.Application.Abstraction
{
    public class ClientDocumentToDownloadDTO
    {
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
