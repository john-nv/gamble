using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class Add_Extraproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Gamble.Transactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Gamble.Rounds",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Gamble.RoundDetails",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Gamble.Games",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Gamble.Chips",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "Gamble.Categories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Gamble.Transactions");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Gamble.Rounds");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Gamble.RoundDetails");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Gamble.Games");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Gamble.Chips");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "Gamble.Categories");
        }
    }
}
