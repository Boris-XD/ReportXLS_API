using ClosedXML.Excel;
using System.Data;
using System.Reflection;

namespace ReportXLS.ExcelTemplates
{
    public class ExcelBase : XLWorkbook
    {
        public ExcelBase(string name)
            : base()
        {
            var worksheet = Worksheets.Add(name);
            worksheet.Style.Font.FontName = "Calibri";
            worksheet.Style.Font.FontSize = 10;

            SetRowHeaderFooterFormat(worksheet.Row(1));
            SetColumnFormatDescription(worksheet.Column("A"));
        }

        public static void SetColumnFormatDescription(IXLColumn column)
        {
            column.Width = 40;
            column.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        }

        public static void SetColumnFormatNumber(IXLColumn column)
        {
            column.Width = 15;
            column.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        }

        public static void SetColumnFormatTextCenter(IXLColumn column)
        {
            column.Width = 20;
            column.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        }

        public static void SetRowHeaderFooterFormat(IXLRow row)
        {
            row.Style.Font.Bold = true;
            row.Style.Fill.BackgroundColor = XLColor.FromHtml("#F4F4F4");
        }

        public static void SetCellsCurrencyFormat(IXLRange cells)
        {
            cells.Style.NumberFormat.Format = "$ #,##0.00";
            cells.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        }

        public static void SetBorder(IXLRange cells)
        {
            cells.Style.Border.BottomBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            cells.Style.Border.TopBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            cells.Style.Border.LeftBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
            cells.Style.Border.RightBorder = ClosedXML.Excel.XLBorderStyleValues.Thin;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);

            // Get all the properties
            var propertiesInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in propertiesInfos)
            {
                // Setting column names as Property names
                dataTable.Columns.Add(property.Name);
                dataTable.Columns[property.Name].DataType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            }

            foreach (var item in items)
            {
                DataRow row = dataTable.NewRow();

                foreach (PropertyInfo info in typeof(T).GetProperties())
                {
                    row[info.Name] = info.GetValue(item, null) ?? DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            // put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
