namespace CRM.Application.Abstraction
{
    public class AddClientVM
    {
        public string Name { get; set; }
        public string Nip { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public bool IsActive { get; set; }
        public decimal Rating { get; set; }
    }
}
