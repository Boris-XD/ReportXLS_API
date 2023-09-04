using ReportXLS.Services.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReportXLS.Services
{
    public class ReadJson
    {
        public static List<NoteResult> NoteResults()
        {
            return new List<NoteResult>
            {
                NoteResult(),
                NoteResult(),
                NoteResult(),
                NoteResult(),
                NoteResult(),
                NoteResultNull(),
                NoteResultNull(),
                NoteResultNull(),
                NoteResultNull(),
                NoteResultNull(),
            };        
        }

        public static NoteResult NoteResult()
        {
            return new NoteResult()
            {
                Id = 1,
                Note = "This is the next note test new note new with another params",
                CreatedAt = DateTime.UtcNow,
                IsHighPriority = true,
                OccupancyLabel = "Label",
                OccupancyNumber = "Number",
                CompanyOrFamilyName = "My Family Name",
                UserId = 1,
                UserName = "Johann Sebastian"
            };
        }

        public static NoteResult NoteResultNull()
        {
            return new NoteResult()
            {
                Id = 1,
                Note = null,
                CreatedAt = DateTime.UtcNow,
                IsHighPriority = false,
                OccupancyLabel = null,
                OccupancyNumber = null,
                CompanyOrFamilyName = null,
                UserId = 1,
                UserName = "Johann Sebastian"
            };
        }

        public static List<NoteResult>? ReadJsonFile()
        {
            string json = File.ReadAllText("notes.json");

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Hace que las propiedades coincidan con mayúsculas y minúsculas
                Converters = { new DateTimeConverter() }, // Agrega un convertidor personalizado para DateTime
            };

            var enumerable = JsonSerializer.Deserialize<List<NoteResult>>(json, options);
            return enumerable;
        }
    }

    class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TryGetDateTime(out var dateTime))
            {
                return dateTime;
            }
            else if (DateTime.TryParse(reader.GetString(), out dateTime))
            {
                return dateTime;
            }
            else
            {
                throw new JsonException("No se pudo convertir el valor de DateTime.");
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ss.fffffffZ"));
        }
    }
}
