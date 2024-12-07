using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPBus_API.Migrations
{
    /// <inheritdoc />
    public partial class addDailyGateAcc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyGateAcc",
                columns: table => new
                {
                    GateCode = table.Column<string>(type: "varchar(4)", nullable: false),
                    AccDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IncTotalAmt = table.Column<double>(type: "float", nullable: true),
                    ExpTotalAmt = table.Column<double>(type: "float", nullable: true),
                    IncCreditAmt = table.Column<double>(type: "float", nullable: true),
                    IncReceiveAmt = table.Column<double>(type: "float", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyGateAcc", x => new { x.GateCode, x.AccDate });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyGateAcc");
        }
    }
}
