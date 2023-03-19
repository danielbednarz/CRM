using AutoMapper;
using Castle.Core.Internal;
using CRM.Application.Abstraction;
using CRM.Application.Services;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM.WebAPI
{
    
    public class ClientDocumentController : AppController
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;
        private readonly IUserService _userService;
        private readonly IClientDocumentService _clientDocumentService;

        public ClientDocumentController(IMapper mapper, IClientService clientService, IUserService userService, IClientDocumentService clientDocumentService)
        {
            _mapper = mapper;
            _clientService = clientService;
            _userService = userService;
            _clientDocumentService = clientDocumentService;
        }


        [HttpPost("add")]
        public async Task<ActionResult> Add(List<IFormFile> files, int clientId)
        {
           // var files = Request.Form.Files;
            if (files == null)
            {
                return NotFound("File not found");
            }

            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    using MemoryStream memoryStream = new();

                    await file.CopyToAsync(memoryStream);
                    byte[] fileContent = memoryStream.ToArray();
                    AddClientDocumentVM model = new()
                    {
                        Name = file.FileName,
                        Content = fileContent,
                        ClientId = clientId
                    };

                    ClientDocument document = _mapper.Map<ClientDocument>(model);

                    await _clientDocumentService.Add(document);
                }

                return Ok();
            }

            return BadRequest("Wrong file");
        }
    }
}
