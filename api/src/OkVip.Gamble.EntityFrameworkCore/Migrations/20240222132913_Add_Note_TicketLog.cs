using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class Add_Note_TicketLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Gamble.TicketLogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Gamble.TicketLogs");
        }
    }
}
