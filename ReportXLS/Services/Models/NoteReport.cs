namespace ReportXLS.Services.Models
{
    public class NoteReport
    {
        public DateTime? Time { get; set; }
        public string ShiftNote { get; set; }
        public string User { get; set; }
        public string UnitNumber { get; set; }
        public string Priority { get; set; }
    }
}
