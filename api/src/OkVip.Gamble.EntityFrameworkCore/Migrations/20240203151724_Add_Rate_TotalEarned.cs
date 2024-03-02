using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class Add_Rate_TotalEarned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Gamble.RoundDetails",
                type: "numeric",
                nullable: false,
                defaultValue: 1m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalEarned",
                table: "Gamble.RoundDetails",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Gamble.RoundDetails");

            migrationBuilder.DropColumn(
                name: "TotalEarned",
                table: "Gamble.RoundDetails");
        }
    }
}
