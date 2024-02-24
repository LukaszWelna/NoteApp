using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NoteApp.Server.Authorization;
using NoteApp.Server.Entities;
using NoteApp.Server.Exceptions;
using NoteApp.Server.Models;
using System.Security.Claims;
using static Azure.Core.HttpHeader;

namespace NoteApp.Server.Services
{
    public interface INoteService
    {
        public Task<IEnumerable<NoteDto>> GetAllAsync();
        public Task<int> CreateNoteAsync(CreateNoteDto createNoteDto);
        public Task DeleteNoteByIdAsync(int id);
        public Task UpdateNoteByIdAsync(int id, UpdateNoteDto updateNoteDto);
    }
    public class NoteService : INoteService
    {
        private readonly NoteAppContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteService> _logger;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public NoteService(NoteAppContext dbContext, IMapper mapper, ILogger<NoteService> logger,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        
        public async Task<IEnumerable<NoteDto>> GetAllAsync()
        {
            var notes = await _dbContext
                .Notes
                .Where(n => n.UserId == _userContextService.GetUserId)
                .ToListAsync();

            var notesDtos = _mapper.Map<List<NoteDto>>(notes);

            return notesDtos;
        }

        public async Task<int> CreateNoteAsync(CreateNoteDto createNoteDto)
        {
            var note = _mapper.Map<Note>(createNoteDto);
            note.UserId = _userContextService.GetUserId;
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();

            return note.Id;
        }

        public async Task DeleteNoteByIdAsync(int id)
        {
            _logger.LogWarning($"DELETE action invoked on note with id: {id}");

            var note = await _dbContext
                .Notes
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                throw new NotFoundException("Note not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, note,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateNoteByIdAsync(int id, UpdateNoteDto updateNoteDto)
        {
            var note = await _dbContext
                .Notes
                .FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                throw new NotFoundException("Note not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, note,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            note.Title = updateNoteDto.Title;
            note.Content = updateNoteDto.Content;

            await _dbContext.SaveChangesAsync();
        }
    }
}
