using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class Add_IdentityUser_Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Tickets_CreatorId",
                table: "Gamble.Tickets",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Tickets_LastModifierId",
                table: "Gamble.Tickets",
                column: "LastModifierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Users_CreatorId",
                table: "Gamble.Tickets",
                column: "CreatorId",
                principalTable: "Gamble.Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Users_LastModifierId",
                table: "Gamble.Tickets",
                column: "LastModifierId",
                principalTable: "Gamble.Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Users_CreatorId",
                table: "Gamble.Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Users_LastModifierId",
                table: "Gamble.Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Gamble.Tickets_CreatorId",
                table: "Gamble.Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Gamble.Tickets_LastModifierId",
                table: "Gamble.Tickets");
        }
    }
}
