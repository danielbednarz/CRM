using CRM.Application.Abstraction;
using CRM.Application.Abstraction.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace CRM.Application.Services
{
    public class NipService : INipService
    {
        private IConfiguration _configuration { get; set; }

        public NipService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private HttpClient _client = new();

        public async Task<ClientDataDTO> GetClientDataByNip(string nip)
        {
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            string mfApiUrl = _configuration["MfApiUrl"];

            HttpResponseMessage response = await _client.GetAsync($"{mfApiUrl}/search/nip/{nip}?date={currentDate}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var root = JObject.Parse(responseBody);
            var parsedResponse = root["result"]["subject"];

            ClientAddressVM clientAddress = DeserializeAddressData((string)parsedResponse["workingAddress"]);

            ClientDataDTO clientData = new()
            {
                Name = (string)parsedResponse["name"],
                Nip = (string)parsedResponse["nip"],
                Krs = (string)parsedResponse["krs"],
                Regon = (string)parsedResponse["regon"],
                IsActive = (string)parsedResponse["statusVat"] == "Czynny" ? true : false,
                Country = clientAddress.Country,
                City = clientAddress.City,
                PostalCode = clientAddress.PostalCode,
                Street = clientAddress.Street,
                BuildingNumber = clientAddress.BuildingNumber
            };

            return clientData;
        }

        private static ClientAddressVM DeserializeAddressData(string address)
        {
            string[] splittedAddress = address.Trim().Split(',');
            string[] streetWithBuildingNumber = splittedAddress[0].Split(' ');
            string[] cityWithPostalCode = splittedAddress[1].Split(' ');

            string street = streetWithBuildingNumber[0];
            string buildingNumber = streetWithBuildingNumber[1];

            string postalCode = cityWithPostalCode[0];
            string city = cityWithPostalCode[1];

            return new ClientAddressVM()
            {
                Country = "Polska",
                City = city,
                PostalCode = postalCode,
                Street = street,
                BuildingNumber = buildingNumber,
            };
        }
    }
}
