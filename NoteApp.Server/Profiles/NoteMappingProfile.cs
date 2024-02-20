using AutoMapper;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;

namespace NoteApp.Server.Profiles
{
    public class NoteMappingProfile : Profile
    {
        public NoteMappingProfile()
        {
            CreateMap<Note, NoteDto>();
            CreateMap<CreateNoteDto, Note>();
        }
    }
}
