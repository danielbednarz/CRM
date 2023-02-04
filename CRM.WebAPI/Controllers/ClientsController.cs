using AutoMapper;
using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebAPI
{
    public class ClientsController : AppController
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;

        public ClientsController(IMapper mapper, IClientService clientService)
        {
            _mapper = mapper;
            _clientService = clientService;
        }


        [HttpPost("addClient")]
        public async Task<ActionResult> AddClient(AddClientVM model)
        {
            if (model == null)
                return NotFound("Error add client");

            if (await _clientService.CheckIsClientExistsByNip(model.Nip))
            {
                return BadRequest("Client with given Tax ID already exists");
            }

            var client = _mapper.Map<Client>(model);
            var addedClientId = await _clientService.AddClient(client);

            return Ok(addedClientId);
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
    }
}
