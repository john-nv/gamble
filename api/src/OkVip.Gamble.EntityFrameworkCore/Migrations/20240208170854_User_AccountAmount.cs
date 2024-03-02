using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class User_AccountAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AccountBalance",
                table: "Gamble.Users",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountBalance",
                table: "Gamble.Users");
        }
    }
}
