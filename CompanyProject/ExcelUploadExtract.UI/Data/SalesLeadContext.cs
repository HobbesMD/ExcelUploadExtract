using ExcelUploadExtract.UI.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelUploadExtract.UI.Data
{
    public class SalesLeadContext : DbContext
    {
        public DbSet<SalesLead> SalesLeads { get; set; }

        public SalesLeadContext(DbContextOptions<SalesLeadContext> options)
            : base(options)
        {
        }
    }
}
