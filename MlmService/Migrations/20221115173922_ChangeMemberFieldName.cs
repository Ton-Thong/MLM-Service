using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlmService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMemberFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Members",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Members",
                newName: "Lastname");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
