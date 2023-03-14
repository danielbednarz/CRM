using CRM.Application.Abstraction;
using CRM.Infrastructure.Domain;

namespace CRM.Application.Abstraction
{
    public interface IClientNotesService
    {
        Task<bool> AddNote(ClientNote note);
        Task EditNote(int noteId, string note);
        Task DeleteNote(int noteId);
        Task<List<ClientNoteDTO>> GetClientNotes(int clientId);
    }
}
