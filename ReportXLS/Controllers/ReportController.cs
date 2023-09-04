using System;
using System.IO;
using System.Text.Json;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReportXLS.Repositories.Context;
using ReportXLS.Repositories.Models;
using ReportXLS.Services.Models;
using System.Data;
using ReportXLS.Services;

namespace ReportXLS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly NotesDbContext _notesDbContext;
        public IConfiguration _configuration { get; set; }

        public ReportController(NotesDbContext notesDbContext, IConfiguration configuration)
        {
            _notesDbContext = notesDbContext;
            _configuration = configuration;
        }


        [HttpGet("notes")]
        public async Task<IActionResult> GetNoteResult()
        {

            var notesResult = ReadJson.NoteResults();

            using ( var workbook = new ExcelDetails(notesResult))
            {
                MemoryStream memoryStream = new MemoryStream();
                workbook.SaveAs(memoryStream);
                return File(memoryStream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "NAmess");
            }
        }


        [HttpGet]
        public async Task<FileResult> GetResponse()
        {
            var notes = await _notesDbContext.Notes.ToListAsync();

            return GenerateExcel("notes.xlsx", notes);
        }

        private FileResult GenerateExcel(string fileName, IEnumerable<Note> notes)
        {
            DataTable dataTable = new DataTable("notes");

            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id", typeof(int)),
                new DataColumn("Note", typeof(string)),
                new DataColumn("Description", typeof(string)),
                new DataColumn("IsHighPriority", typeof(bool)),
                new DataColumn("CreatedAt", typeof(DateTime)),
                new DataColumn("Email", typeof(string))
            });

            foreach(var note in notes)
            {
                dataTable.Rows.Add(note.Id, note.Notes, note.Description, note.IsHighPriority, note.CreatedAt, note.Email);
            }

            using(XLWorkbook workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(dataTable);

                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }
    }
}
