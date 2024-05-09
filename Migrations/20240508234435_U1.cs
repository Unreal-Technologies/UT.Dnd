using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UT.Dnd.Migrations
{
    /// <inheritdoc />
    public partial class U1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "UT.Dnd.Map",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "UT.Dnd.Map");
        }
    }
}
