using CsvHelper;
using ExcelUploadExtract.UI.Models;
using OfficeOpenXml;
using System.Globalization;
using System.IO;

namespace ExcelUploadExtract.UI
{
    public class FileUpload
    {
        private readonly string _filePath;
        private string _fileName => Path.GetFileName(_filePath);
        private readonly string _contentType;

        public FileUpload(string path, string contentType)
        {
            _filePath = path;
            _contentType = contentType;
        }

        public IEnumerable<SalesLead> GetSalesLeads()
        {
            if (_contentType == "text/csv")
                return ReadCSV();

            if (_contentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                return ReadXLSX();

            throw new NotImplementedException($"Reading of file content type {_contentType} is not implemented");
        }

        private IEnumerable<SalesLead> ReadCSV()
        {
            using (var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<SalesLead>();
                return records.ToList();
            }
        }

        private IEnumerable<SalesLead> ReadXLSX()
        {
            ExcelPackage.License.SetNonCommercialPersonal("Michael Dykema");
            using (ExcelPackage package = new ExcelPackage(_filePath))
            {
                ExcelWorksheet ws = package.Workbook.Worksheets.FirstOrDefault();
                int colCount = ws.Dimension.End.Column;
                int rowCount = ws.Dimension.End.Row;

                var expectedHeaders = new List<string>
                {
                    nameof(SalesLead.QuoteSentDate),
                    nameof(SalesLead.SalesPerson),
                    nameof(SalesLead.ProjectName),
                    nameof(SalesLead.QuoteAmount)
                };

                // Validate headers
                var headers = new Dictionary<string, int>();
                for (int col = 1; col <= colCount; col++)
                {
                    var header = ws.Cells[1, col].Value?.ToString();
                    if (!string.IsNullOrEmpty(header))
                        headers.Add(header, col);
                }

                foreach (var header in expectedHeaders)
                {
                    if (!headers.ContainsKey(header))
                        throw new Exception($"{_fileName} does not contain header {header}");
                }

                var leads = new List<SalesLead>();
                for (int row = 2; row <= rowCount; row++)
                {
                    leads.Add(new SalesLead
                    {
                        QuoteSentDate = DateTime.FromOADate((double)ws.Cells[row, headers[nameof(SalesLead.QuoteSentDate)]].Value),
                        SalesPerson = ws.Cells[row, headers[nameof(SalesLead.SalesPerson)]].Value.ToString(),
                        ProjectName = ws.Cells[row, headers[nameof(SalesLead.ProjectName)]].Value?.ToString(),
                        QuoteAmount = (double)ws.Cells[row, headers[nameof(SalesLead.QuoteAmount)]].Value
                    });
                }

                return leads;
            }
        }
    }
}
