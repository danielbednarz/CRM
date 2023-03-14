using AutoMapper;
using Castle.Core.Internal;
using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRM.WebAPI
{
    public class ClientNotesController : AppController
    {
        private readonly IMapper _mapper;
        private readonly IClientService _clientService;
        private readonly IClientNotesService _clientNotesService;
        private readonly IUserService _userService;

        public ClientNotesController(IMapper mapper, IClientService clientService, IClientNotesService clientNotesService, IUserService userService)
        {
            _mapper = mapper;
            _clientService = clientService;
            _clientNotesService = clientNotesService;
            _userService = userService;
        }


        [HttpPost("add")]
        public async Task<ActionResult> Add(AddClientNoteVM model)
        {
            if (model == null || model.Title.IsNullOrEmpty() || model.Content.IsNullOrEmpty())
            {
                return NotFound("Error add client note");
            }

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetUserByUsernameAsync(username);

            var clientNote = _mapper.Map<ClientNote>(model);
            clientNote.UserId = user.Id;

            bool isSuccess = await _clientNotesService.AddNote(clientNote);    
            if (!isSuccess)
            {
                return BadRequest("Cannot add note");
            }

            return Ok();
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Edit(EditClientVM model)
        {
            if (model == null)
            {
                return NotFound("Error add client");
            }

            var client = _mapper.Map<Client>(model);

            await _clientService.EditClient(client);

            return Ok();
        }

        [HttpDelete("delete")]
        public ActionResult Delete(Guid noteId)
        {
            _clientNotesService.DeleteNote(noteId);

            return Ok();
        }

        [HttpGet("getClientNotes")]
        public async Task<ActionResult> GetClientNotes(int clientId)
        {
            var clientNotes = await _clientNotesService.GetClientNotes(clientId);

            return Ok(clientNotes);
        }
    }
}
