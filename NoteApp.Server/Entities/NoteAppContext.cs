using Microsoft.EntityFrameworkCore;

namespace NoteApp.Server.Entities
{
    public class NoteAppContext : DbContext
    {
        public NoteAppContext(DbContextOptions<NoteAppContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(eb =>
            {
                eb.Property(u => u.FirstName)
                .HasMaxLength(50);

                eb.Property(u => u.LastName)
                .HasMaxLength(50);

                eb.HasMany(u => u.Notes)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId);
            });

            modelBuilder.Entity<Note>(eb =>
            {
                eb.Property(n => n.Title)
                .HasMaxLength(50);

                eb.Property(n => n.Content)
                .HasMaxLength(400);
            });
        }
    }
}
