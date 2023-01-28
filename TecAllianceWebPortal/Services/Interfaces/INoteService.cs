using TecAllianceWebPortal.Communication.Requests;
using TecAllianceWebPortal.Communication.Responses;

namespace TecAllianceWebPortal.Services.Interfaces
{
    public interface INoteService
    {
        public Task<IEnumerable<NoteResponse>> GetAllNotesForUser(string email);
        public Task<NoteResponse> CreateNote(NoteRequest request, string email);
        public Task<NoteResponse> UpdateNote(NoteRequest request, string email);
        public Task DeleteNote(int id, string email);
    }
}
