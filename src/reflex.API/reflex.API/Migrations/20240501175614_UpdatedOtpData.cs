using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reflex.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOtpData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Otps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Otps");
        }
    }
}
