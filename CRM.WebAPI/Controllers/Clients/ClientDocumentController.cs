using AutoMapper;
using Castle.Core.Internal;
using CRM.Application.Abstraction;
using CRM.Application.Services;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        public async Task<ActionResult> Add()
        {
            var files = Request.Form.Files;
            var clientId = Request.Form.FirstOrDefault(x => x.Key == "clientId");
            if (files == null)
            {
                return NotFound("Files not found");
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
                        ContentType = file.ContentType,
                        ClientId = !string.IsNullOrEmpty(clientId.Value) ? int.Parse(clientId.Value) : throw new Exception("Invalid client id")
                    };

                    ClientDocument document = _mapper.Map<ClientDocument>(model);

                    await _clientDocumentService.Add(document);
                }

                return Ok();
            }

            return BadRequest("Wrong file");
        }

        [HttpGet("getClientDocuments")]
        public async Task<ActionResult> GetClientDocuments(int clientId)
        {
            List<ClientDocumentDTO> data = await _clientDocumentService.GetClientDocuments(clientId);

            return Ok(data);
        }

        [HttpDelete("delete")]
        public ActionResult Delete(Guid documentId)
        {
            _clientDocumentService.DeleteDocument(documentId);

            return Ok();
        }
    }
}
