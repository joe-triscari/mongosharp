using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace MongoSharp
{
    public class ExcelExport
    {

        public void exportToExcel(DataGridView dt)
        {
            try
            {
                var sb = new StringBuilder();

                var headers = dt.Columns.Cast<DataGridViewColumn>();
                sb.Append(string.Join(",", headers.Select(column => "\"" + column.HeaderText + "\"")));
                sb.AppendLine();

                foreach (DataGridViewRow row in dt.Rows)
                {
                    var cells = row.Cells.Cast<DataGridViewCell>();
                    sb.Append(string.Join(",", cells.Select(cell => "\"" + cell.Value + "\"")));
                    sb.AppendLine();
                }

                //using (var memoryStream = new MemoryStream())
                //{
                //    using (var writer = new StreamWriter(memoryStream))
                //    {
                //        writer.Write(sb.ToString());
                //        memoryStream.Seek(0, SeekOrigin.Begin);
                //        OpenAndAddToSpreadsheetStream(memoryStream);
                //    }                
                //}

                string filename = System.IO.Path.GetTempFileName().Replace(".tmp", ".csv");
                File.WriteAllText(filename, sb.ToString());

                var proc = new Process { StartInfo = new ProcessStartInfo("excel.exe", filename) };
                proc.Start();
            }
            catch { }            
        }

        public static void OpenAndAddToSpreadsheetStream(Stream stream)
        {
            // Open a SpreadsheetDocument based on a stream.
            SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(stream, true, new OpenSettings{});

            // Add a new worksheet.
            var newWorksheetPart = spreadsheetDocument.WorkbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());
            newWorksheetPart.Worksheet.Save();

            var sheets = spreadsheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>();
            string relationshipId = spreadsheetDocument.WorkbookPart.GetIdOfPart(newWorksheetPart);

            // Get a unique ID for the new worksheet.
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Any())
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            // Give the new worksheet a name.
            string sheetName = "Sheet" + sheetId;

            // Append the new worksheet and associate it with the workbook.
            var sheet = new Sheet { Id = relationshipId, SheetId = sheetId, Name = sheetName };
            sheets.Append(sheet);
            spreadsheetDocument.WorkbookPart.Workbook.Save();

            // Close the document handle.
            spreadsheetDocument.Close();

            // Caller must close the stream.
        }
    }
}
