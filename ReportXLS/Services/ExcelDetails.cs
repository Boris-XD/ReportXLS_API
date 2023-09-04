using ReportXLS.ExcelTemplates;
using ReportXLS.Services.Models;
using System.Data;

namespace ReportXLS.Services
{
    public class ExcelDetails : ExcelBase
    {
        public const string Name = "Note Reports";
        public List<NoteResult> _noteResults { get; set; }

        public ExcelDetails(List<NoteResult> noteResults) : base(Name)
        {
            _noteResults = noteResults;

            var worksheet = Worksheet(Name);

            worksheet.Cell("A1").Value = "Time";
            worksheet.Cell("B1").Value = "Shift note";
            worksheet.Cell("C1").Value = "User";
            worksheet.Cell("D1").Value = "Unit number";
            worksheet.Cell("E1").Value = "Priority";
            worksheet.Cell("F1").Value = "Location";

            SetColumnFormatTextCenter(worksheet.Column("A"));
            SetColumnFormatDescription(worksheet.Column("B"));
            SetColumnFormatTextCenter(worksheet.Column("C"));
            SetColumnFormatTextCenter(worksheet.Column("D"));
            SetColumnFormatTextCenter(worksheet.Column("E"));
            SetColumnFormatTextCenter(worksheet.Column("F"));

            DataTable dataTable = new DataTable();

            var table = ToDataTable(NoteReport(noteResults));

            DataTableReader dtReader = new DataTableReader(table);
            dataTable.Load(dtReader);

            worksheet.Cell(2, 1).InsertData(dataTable);

            SetBorder(worksheet.Range($"A1:F{noteResults.Count+1}"));
        }

        private List<NoteReport> NoteReport(List<NoteResult> noteResults)
        {
            return noteResults.Select(noteResult => new NoteReport
            {
                Time = noteResult.CreatedAt,
                ShiftNote = noteResult.Note != null ? noteResult.Note : string.Empty,
                User = noteResult.UserName,
                UnitNumber = noteResult?.OccupancyLabel + " " + noteResult?.OccupancyNumber,
                Priority = noteResult.IsHighPriority ? "High priority" : "Normal",
            }).ToList();
        }



    }
}
