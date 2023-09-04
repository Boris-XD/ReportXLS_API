using Microsoft.EntityFrameworkCore;
using ReportXLS.Repositories.Models;

namespace ReportXLS.Repositories.Context
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
        {
        }

        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NoteConfiguration());
        }
    }
}
