using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkParty.Migrations
{
    public partial class RemoveRoomCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rooms_Code",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Rooms");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Code",
                table: "Rooms",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Code",
                table: "Rooms",
                column: "Code");
        }
    }
}
