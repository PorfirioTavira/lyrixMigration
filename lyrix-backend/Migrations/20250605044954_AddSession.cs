using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lyrix_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccessToken",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessToken",
                table: "Sessions");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Sessions");
        }
    }
}
