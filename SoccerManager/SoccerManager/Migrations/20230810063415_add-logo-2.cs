using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SoccerManager.Migrations
{
    /// <inheritdoc />
    public partial class addlogo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogoUrl",
                table: "Team",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoUrl",
                table: "Team");
        }
    }
}
