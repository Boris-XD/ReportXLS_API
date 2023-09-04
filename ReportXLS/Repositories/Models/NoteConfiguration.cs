using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReportXLS.Repositories.Models
{
    public class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> entity)
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("current_timestamp");
        }
    }
}
