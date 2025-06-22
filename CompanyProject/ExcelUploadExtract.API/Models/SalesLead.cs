namespace ExcelUploadExtract.API.Models
{
    public class SalesLead
    {
        public required DateTime QuoteSentDate { get; set; }
        public required string SalesPerson { get; set; }
        public required string ProjectName { get; set; }
        public required double QuoteAmount { get; set; }
    }
}
