using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI
{
    [Authorize]
    public class ClientsController : AppController
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;
        private readonly INipService _nipService;
        private readonly IClientImportService _clientImportService;
        private readonly IClientAddressService _clientAddressService;

        public ClientsController(IMapper mapper, IClientService clientService, INipService nipService, IClientImportService clientImportService, IClientAddressService clientAddressService)
        {
            _mapper = mapper;
            _clientService = clientService;
            _nipService = nipService;
            _clientImportService = clientImportService;
            _clientAddressService = clientAddressService;
        }


        [HttpPost("addClient")]
        public async Task<ActionResult> AddClient(AddClientVM model)
        {
            if (model == null)
            {
                return NotFound("Error add client");
            }

            if (await _clientService.CheckIsClientExistsByNip(model.Nip))
            {
                return BadRequest("Client with given Tax ID already exists");
            }

            var client = _mapper.Map<Client>(model);
            var addedClientId = await _clientService.AddClient(client);

            return Ok(addedClientId);
        }

        [HttpPut("editClient")]
        public async Task<ActionResult> EditClient(EditClientVM model)
        {
            if (model == null)
            {
                return NotFound("Error add client");
            }

            var client = _mapper.Map<Client>(model);

            await _clientService.EditClient(client);

            return Ok();
        }

        [HttpGet("getClientById")]
        public async Task<ActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientById(id);

            return Ok(client);
        }

        [HttpGet("getClientByNip")]
        public async Task<ActionResult> GetClientByNip(string nip)
        {
            var client = await _clientService.GetClientByNip(nip);

            return Ok(client);
        }

        [HttpGet("getClientDataFromWLRegistry")]
        public async Task<ActionResult> GetClientDataFromWLRegistry(string nip)
        {
            var client = await _nipService.GetClientDataByNip(nip);

            return Ok(client);
        }

        [HttpGet("getClients")]
        public async Task<ActionResult> GetClients()
        {
            var clients = await _clientService.GetClients();

            return Ok(clients);
        }

        [HttpDelete("deleteClient")]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var isClientDeleted = _clientService.DeleteClient(id);

            if (!isClientDeleted)
            {
                return NotFound("There is no client with given id");
            }

            return Ok();
        }

        [HttpPost("importClientsFromFile")]
        public async Task<ActionResult> ImportClientsFromFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                using MemoryStream memoryStream = new();

                await file.CopyToAsync(memoryStream);
                byte[] fileContent = memoryStream.ToArray();

                int addedClientsCount = await _clientImportService.ImportClientsFromXlsxFile(fileContent);

                return Ok(addedClientsCount );
            }

            return BadRequest("Wrong file");
        }

        [HttpPost("addPhoneNumber")]
        public async Task<ActionResult> AddPhoneNumber(ClientPhoneNumberVM model)
        {
            await _clientAddressService.AddPhoneNumber(model.ClientId, model.PhoneNumber);

            return Ok();
        }

        [HttpDelete("deletePhoneNumber")]
        public ActionResult DeletePhoneNumber(int clientId, Guid phoneNumberId)
        {
            bool isSuccess = _clientAddressService.DeletePhoneNumber(clientId, phoneNumberId);
            if (!isSuccess)
            {
                return NotFound("Error when trying to delete phone number");
            }

            return Ok();
        }

        [HttpPost("addEmail")]
        public async Task<ActionResult> AddEmail(ClientEmailVM model)
        {
            bool isSuccess = await _clientAddressService.AddEmail(model.ClientId, model.Email);
            if (!isSuccess)
            {
                return NotFound("Error during add email");
            }

            return Ok();
        }

        [HttpDelete("deleteEmail")]
        public ActionResult DeleteEmail(int clientId, Guid emailId)
        {
            bool isSuccess = _clientAddressService.DeleteEmail(clientId, emailId);
            if (!isSuccess)
            {
                return NotFound("Error when trying to delete phone number");
            }

            return Ok();
        }
    }
}
