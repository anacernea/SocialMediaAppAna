﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaAppAna.Migrations
{
    /// <inheritdoc />
    public partial class video1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Video",
                table: "Posts");
        }
    }
}
