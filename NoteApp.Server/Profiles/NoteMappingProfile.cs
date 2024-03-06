using AutoMapper;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;

namespace NoteApp.Server.Profiles
{
    // Automapper - profiles
    public class NoteMappingProfile : Profile
    {
        public NoteMappingProfile()
        {
            CreateMap<Note, NoteDto>();
            CreateMap<CreateNoteDto, Note>();
            CreateMap<RegisterUserDto, User>();
        }
    }
}
