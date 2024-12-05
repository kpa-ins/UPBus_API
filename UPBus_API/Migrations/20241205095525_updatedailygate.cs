using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPBus_API.Migrations
{
    /// <inheritdoc />
    public partial class updatedailygate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DailyGateIncome",
                type: "nvarchar(500)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GateCode",
                table: "DailyGateIncome",
                type: "varchar(4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GateCode",
                table: "DailyGateExpense",
                type: "varchar(4)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DailyGateIncome");

            migrationBuilder.DropColumn(
                name: "GateCode",
                table: "DailyGateIncome");

            migrationBuilder.DropColumn(
                name: "GateCode",
                table: "DailyGateExpense");
        }
    }
}
