using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaAppAna.Migrations
{
    /// <inheritdoc />
    public partial class profilGrupuri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "visibility",
                table: "AspNetUsers",
                newName: "Visibility");

            migrationBuilder.AlterColumn<string>(
                name: "Visibility",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Visibility",
                table: "AspNetUsers",
                newName: "visibility");

            migrationBuilder.AlterColumn<string>(
                name: "visibility",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
