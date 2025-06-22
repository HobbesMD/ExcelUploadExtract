using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExcelUploadExtract.UI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesLeads",
                columns: table => new
                {
                    QuoteSentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalesPerson = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    QuoteAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesLeads", x => new { x.QuoteSentDate, x.SalesPerson, x.ProjectName });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesLeads");
        }
    }
}
