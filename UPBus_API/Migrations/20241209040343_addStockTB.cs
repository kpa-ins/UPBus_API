using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPBus_API.Migrations
{
    /// <inheritdoc />
    public partial class addStockTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockHistory",
                columns: table => new
                {
                    RegNo = table.Column<string>(type: "varchar(15)", nullable: false),
                    StockCode = table.Column<string>(type: "varchar(3)", nullable: true),
                    StockDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    StockType = table.Column<string>(type: "varchar(8)", nullable: true),
                    Qty = table.Column<double>(type: "float", nullable: true),
                    LastPrice = table.Column<double>(type: "float", nullable: true),
                    TotalAmt = table.Column<double>(type: "float", nullable: true),
                    BusNo = table.Column<string>(type: "varchar(7)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockHistory", x => x.RegNo);
                });

            migrationBuilder.CreateTable(
                name: "StockMain",
                columns: table => new
                {
                    StockCode = table.Column<string>(type: "varchar(3)", nullable: false),
                    StockName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: true),
                    LastPrice = table.Column<double>(type: "float", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMain", x => x.StockCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockHistory");

            migrationBuilder.DropTable(
                name: "StockMain");
        }
    }
}
