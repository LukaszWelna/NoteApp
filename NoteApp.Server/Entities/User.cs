using Microsoft.EntityFrameworkCore;

namespace NoteApp.Server.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public virtual List<Note> Notes { get; set; } = new List<Note>();
    }
}
