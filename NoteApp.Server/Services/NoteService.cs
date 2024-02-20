using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;

namespace NoteApp.Server.Services
{
    public interface INoteService
    {
        public Task<IEnumerable<NoteDto>> GetAllAsync();
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
    }
}
