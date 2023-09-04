namespace ReportXLS.Services.Models
{
    public class NoteResult
    {
        public int Id { get; set; }

        public string? Note { get; set; }

        public DateTime? CreatedAt { get; set; }

        public bool IsHighPriority { get; set; }

        public string? OccupancyLabel { get; set; }

        public string? OccupancyNumber { get; set; }

        public string? CompanyOrFamilyName { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}
