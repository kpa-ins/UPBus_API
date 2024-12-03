using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPBus_API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDailyPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyPlan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

        }
    }
}
