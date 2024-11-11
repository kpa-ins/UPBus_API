using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPBus_API.Migrations
{
    /// <inheritdoc />
    public partial class AddMasterSetupTB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    BusNo = table.Column<string>(type: "varchar(7)", nullable: false),
                    DriverName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    NoOfSeat = table.Column<int>(type: "int", nullable: true),
                    Owner = table.Column<string>(type: "varchar(5)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.BusNo);
                });

            migrationBuilder.CreateTable(
                name: "DailyPlan",
                columns: table => new
                {
                    DailyPlanID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripCode = table.Column<string>(type: "varchar(3)", nullable: true),
                    TripDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TripTime = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Track = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    BusNo = table.Column<string>(type: "varchar(7)", nullable: true),
                    DriverName = table.Column<string>(type: "varchar(7)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlan", x => x.DailyPlanID);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseType",
                columns: table => new
                {
                    ExpCode = table.Column<string>(type: "varchar(5)", nullable: false),
                    ExpName = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    ExpType = table.Column<string>(type: "varchar(15)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseType", x => x.ExpCode);
                });

            migrationBuilder.CreateTable(
                name: "Gate",
                columns: table => new
                {
                    GateCode = table.Column<string>(type: "varchar(4)", nullable: false),
                    GateName = table.Column<string>(type: "nvarchar(150)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gate", x => x.GateCode);
                });

            migrationBuilder.CreateTable(
                name: "IncomeType",
                columns: table => new
                {
                    IncCode = table.Column<string>(type: "varchar(5)", nullable: false),
                    IncName = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    IncType = table.Column<string>(type: "varchar(15)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeType", x => x.IncCode);
                });

            migrationBuilder.CreateTable(
                name: "TripType",
                columns: table => new
                {
                    TripCode = table.Column<string>(type: "varchar(3)", nullable: false),
                    TripName = table.Column<string>(type: "varchar(50)", nullable: true),
                    TrpType = table.Column<string>(type: "varchar(4)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripType", x => x.TripCode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "DailyPlan");

            migrationBuilder.DropTable(
                name: "ExpenseType");

            migrationBuilder.DropTable(
                name: "Gate");

            migrationBuilder.DropTable(
                name: "IncomeType");

            migrationBuilder.DropTable(
                name: "TripType");
        }
    }
}
