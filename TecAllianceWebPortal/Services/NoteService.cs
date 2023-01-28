using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TecAllianceWebPortal.Communication.Requests;
using TecAllianceWebPortal.Communication.Responses;
using TecAllianceWebPortal.Model;
using TecAllianceWebPortal.Services.Interfaces;

namespace TecAllianceWebPortal.Services
{
    public class NoteService : INoteService
    {
        private readonly PortalDBContext _context;
        private readonly IMapper _mapper;

        public NoteService(PortalDBContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<NoteResponse>> GetAllNotesForUser(string email)
        {
            var list = new List<NoteResponse>();
            var notes = await _context.Notes.Where(f => f.User.Email == email).ToListAsync();
            foreach (var note in notes)
            {
                list.Add(_mapper.Map<NoteResponse>(note));
            }
            return list;
        }

        public async Task<NoteResponse> CreateNote(NoteRequest request, string email)
        {
            var note = _mapper.Map<Note>(request);
            var user = _context.Users.Where(u => u.Email == email).FirstOrDefault();
            note.User = user!;
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
            return _mapper.Map<NoteResponse>(note);
        }

        public async Task<NoteResponse> UpdateNote(NoteRequest request, string email)
        {
            var note = _mapper.Map<Note>(request);
            var user = _context.Users.Where(u => u.Email == email && u.Notes.Any(n => n.Id == note.Id)).FirstOrDefault();

            if (user != null)
            {
                note.User = user!;
                _context.Notes.Update(note);
                await _context.SaveChangesAsync();
                return _mapper.Map<NoteResponse>(note);
            }
            throw new Exception("You can change only your own notes");
        }

        public async Task DeleteNote(int id, string email)
        {
            var note = _context.Notes.Where(f => f.Id == id && f.User.Email == email).FirstOrDefault();

            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Can't delete item");
            }
        }
    }
}
