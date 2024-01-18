using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PitchMatch.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserAndPitch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CvUrl",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Pitch",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Yield",
                table: "Pitch",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CvUrl",
                table: "User");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Pitch");

            migrationBuilder.DropColumn(
                name: "Yield",
                table: "Pitch");
        }
    }
}
