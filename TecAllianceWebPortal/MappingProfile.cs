using AutoMapper;
using TecAllianceWebPortal.Communication.Requests;
using TecAllianceWebPortal.Communication.Responses;
using TecAllianceWebPortal.Model;

namespace TecAllianceWebPortal
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NoteRequest, Note>();
            CreateMap<Note, NoteResponse>();
        }
    }
}
