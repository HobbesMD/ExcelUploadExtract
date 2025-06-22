using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExcelUploadExtract.UI.Models
{
    [PrimaryKey(nameof(QuoteSentDate), nameof(SalesPerson), nameof(ProjectName))]
    public class SalesLead
    {
        public required DateTime QuoteSentDate { get; set; }
        public required string SalesPerson { get; set; }
        public required string ProjectName { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public required double QuoteAmount { get; set; }
    }
}
