using System.ComponentModel.DataAnnotations.Schema;

namespace ReportXLS.Repositories.Models
{
    [Table("Notes")]
    public class Note
    {
        public int Id { get; set; }

        [Column("Note")]
        public string? Notes { get; set; }

        public string Description { get; set; }

        public bool IsHighPriority { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Email { get; set; }
    }
}
