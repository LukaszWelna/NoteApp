using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApp.Server.Entities;
using NoteApp.Server.Models;
using NoteApp.Server.Services;

namespace NoteApp.Server.Controllers
{
    [ApiController]
    [Route("api/notes")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _service;
        public NoteController(INoteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NoteDto>>> GetAllAsync()
        {
            var notesDtos = await _service.GetAllAsync();

            return Ok(notesDtos);
        }

        [HttpPost]
        public async Task<ActionResult> CreateNoteAsync([FromBody] CreateNoteDto createNoteDto)
        {
            var noteId = await _service.CreateNoteAsync(createNoteDto);

            return Created($"/api/notes/{noteId}", null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNoteByIdAsync([FromRoute] int id)
        {
            await _service.DeleteNoteByIdAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateNoteByIdAsync([FromRoute] int id, 
            [FromBody] UpdateNoteDto updateNoteDto)
        {
            await _service.UpdateNoteByIdAsync(id, updateNoteDto);

            return NoContent();
        }
    }
}
