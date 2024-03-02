using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class Update_Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatorUsername",
                table: "Gamble.Tickets",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorName",
                table: "Gamble.Tickets",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUsername",
                table: "Gamble.TicketLogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorName",
                table: "Gamble.TicketLogs",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.TicketLogs_CreatorId",
                table: "Gamble.TicketLogs",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamble.TicketLogs_Gamble.Users_CreatorId",
                table: "Gamble.TicketLogs",
                column: "CreatorId",
                principalTable: "Gamble.Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamble.TicketLogs_Gamble.Users_CreatorId",
                table: "Gamble.TicketLogs");

            migrationBuilder.DropIndex(
                name: "IX_Gamble.TicketLogs_CreatorId",
                table: "Gamble.TicketLogs");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUsername",
                table: "Gamble.Tickets",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorName",
                table: "Gamble.Tickets",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorUsername",
                table: "Gamble.TicketLogs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatorName",
                table: "Gamble.TicketLogs",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
