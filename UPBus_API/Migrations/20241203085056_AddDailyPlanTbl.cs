using Microsoft.EntityFrameworkCore.Migrations;
using UPBus_API.Entities;

#nullable disable

namespace UPBus_API.Migrations
{
    /// <inheritdoc />
    public partial class AddDailyPlanTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyPlan",
                columns: table => new
                {
                    RegNo = table.Column<string>(type: "varchar(8)", nullable: false),
                    TrackCode = table.Column<string>(type: "varchar(15)", nullable: true),
                    TripDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TripTime = table.Column<string>(type: "nvarchar(5)", nullable: true),
                    Track = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    BusNo = table.Column<string>(type: "varchar(7)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    TrackType = table.Column<string>(type: "varchar(4)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedUser = table.Column<string>(type: "varchar(50)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedUser = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyPlan", x => x.RegNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "DailyPlan");

        }
    }
}
