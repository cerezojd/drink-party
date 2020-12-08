using Microsoft.EntityFrameworkCore.Migrations;

namespace DrinkParty.Migrations
{
    public partial class RoomGameMode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameMode",
                table: "Rooms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameMode",
                table: "Rooms");
        }
    }
}
