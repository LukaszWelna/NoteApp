﻿using System.ComponentModel.DataAnnotations;

namespace NoteApp.Server.Models
{
    public class CreateNoteDto
    {
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [MaxLength(400)]
        public string Content { get; set; }
    }
}
