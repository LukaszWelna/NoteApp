using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NoteApp.Server.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }
}
