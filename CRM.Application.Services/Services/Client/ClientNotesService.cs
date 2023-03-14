﻿using CRM.Application.Abstraction;
using CRM.Data.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Services
{
    public class ClientNotesService : IClientNotesService
    {
        private readonly IClientNotesRepository _clientNotesRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public ClientNotesService(IClientNotesRepository clientNotesRepository, IClientRepository clientRepository, IUserRepository userRepository)
        {
            _clientNotesRepository = clientNotesRepository;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddNote(ClientNote note)
        {
            Client client = await _clientRepository.GetClientById(note.ClientId);
            if (client == null)
            {
                throw new Exception("Client with given Id not exists");
            }

            _clientNotesRepository.Add(note);
            bool isSuccess = await _clientNotesRepository.SaveAsync() > 0;

            return isSuccess;
        }

        public Task DeleteNote(int noteId)
        {
            throw new NotImplementedException();
        }

        public Task EditNote(int noteId, string note)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ClientNoteDTO>> GetClientNotes(int clientId)
        {
            Client client = await _clientRepository.GetClientById(clientId);
            if (client == null)
            {
                throw new Exception("Client with given Id not exists");
            }

            List<ClientNote> clientNotes = await _clientNotesRepository.GetClientNotes(clientId);
            List<ClientNoteDTO> result = new();

            foreach (ClientNote clientNote in clientNotes)
            {
                AppUser user = await _userRepository.GetByIdAsync(clientNote.UserId);

                ClientNoteDTO clientNoteDTO = new()
                {
                    Id = clientNote.Id,
                    Content = clientNote.Content,
                    CreatedDate = clientNote.CreatedDate,
                    FirstName = user.FirstName, 
                    LastName = user.LastName
                };

                result.Add(clientNoteDTO);
            }

            return result;
        }

    }
}
