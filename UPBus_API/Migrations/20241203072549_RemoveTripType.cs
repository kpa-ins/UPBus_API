using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UPBus_API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTripType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
