using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UT.Dnd.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UT.Dnd.Map",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    TransStartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UT.Dnd.Map", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UT.Dnd.Map_Shared.User_UserId",
                        column: x => x.UserId,
                        principalTable: "Shared.User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");


            migrationBuilder.CreateIndex(
                name: "IX_UT.Dnd.Map_UserId",
                table: "UT.Dnd.Map",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UT.Dnd.Map");
        }
    }
}
