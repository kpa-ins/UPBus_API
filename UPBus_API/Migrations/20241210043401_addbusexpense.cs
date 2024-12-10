using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPBus_API.Migrations
{
    /// <inheritdoc />
    public partial class addbusexpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancel",
                table: "StockHistory",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusExpense",
                columns: table => new
                {
                    ExpNo = table.Column<string>(type: "varchar(15)", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpCode = table.Column<string>(type: "varchar(6)", nullable: true),
                    BusNo = table.Column<string>(type: "varchar(7)", nullable: true),
                    Qty = table.Column<double>(type: "float", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    TotalAmt = table.Column<double>(type: "float", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusExpense", x => x.ExpNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusExpense");

            migrationBuilder.DropColumn(
                name: "IsCancel",
                table: "StockHistory");
        }
    }
}
