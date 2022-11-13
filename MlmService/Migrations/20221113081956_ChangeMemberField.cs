using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MlmService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMemberField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subdistrict",
                table: "Members",
                newName: "Amphure");

            migrationBuilder.AlterColumn<int>(
                name: "Zipcode",
                table: "Members",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmphureId",
                table: "Members",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Members",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Members",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmphureId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Members");

            migrationBuilder.RenameColumn(
                name: "Amphure",
                table: "Members",
                newName: "Subdistrict");

            migrationBuilder.AlterColumn<string>(
                name: "Zipcode",
                table: "Members",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
