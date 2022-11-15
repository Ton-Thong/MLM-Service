using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlmService.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMemberField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Idcard",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Members");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Idcard",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Nationality",
                table: "Members",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
