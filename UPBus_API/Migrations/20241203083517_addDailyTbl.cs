using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPBus_API.Migrations
{
    /// <inheritdoc />
    public partial class addDailyTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyGateExpense",
                columns: table => new
                {
                    ExpNo = table.Column<string>(type: "varchar(15)", nullable: false),
                    ExpDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ExpCode = table.Column<string>(type: "varchar(6)", nullable: true),
                    PaidType = table.Column<string>(type: "varchar(6)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyGateExpense", x => x.ExpNo);
                });

            migrationBuilder.CreateTable(
                name: "DailyGateIncome",
                columns: table => new
                {
                    IncNo = table.Column<string>(type: "varchar(15)", nullable: false),
                    IncDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IncCode = table.Column<string>(type: "varchar(6)", nullable: true),
                    PaidType = table.Column<string>(type: "varchar(6)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyGateIncome", x => x.IncNo);
                });

            migrationBuilder.CreateTable(
                name: "GasStation",
                columns: table => new
                {
                    GSCode = table.Column<string>(type: "varchar(5)", nullable: false),
                    GSName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    TotalBalance = table.Column<double>(type: "float", nullable: true),
                    Unit = table.Column<string>(type: "varchar(5)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GasStation", x => x.GSCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyGateExpense");

            migrationBuilder.DropTable(
                name: "DailyGateIncome");

            migrationBuilder.DropTable(
                name: "GasStation");
        }
    }
}
