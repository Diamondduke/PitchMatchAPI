using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PitchMatch.Migrations
{
    /// <inheritdoc />
    public partial class coordinateVariablesPersonalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "PersonalData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "PersonalData",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "PersonalData");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "PersonalData");
        }
    }
}
