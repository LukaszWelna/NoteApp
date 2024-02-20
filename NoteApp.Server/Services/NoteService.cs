using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;

namespace NoteApp.Server.Services
{
    public interface INoteService
    {
        public Task<IEnumerable<NoteDto>> GetAllAsync();
        public Task<int> CreateNoteAsync(CreateNoteDto createNoteDto);
    }
    public class NoteService : INoteService
    {
        private readonly NoteAppContext _dbContext;
        private readonly IMapper _mapper;
        public NoteService(NoteAppContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<NoteDto>> GetAllAsync()
        {
            var notes = await _dbContext
                .Notes
                .ToListAsync();

            var notesDtos = _mapper.Map<List<NoteDto>>(notes);

            return notesDtos;
        }

        public async Task<int> CreateNoteAsync(CreateNoteDto createNoteDto)
        {
            var note = _mapper.Map<Note>(createNoteDto);
            note.UserId = 1;
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();

            return note.Id;
        }
    }
}
