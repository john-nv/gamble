using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class Add_CurrentRoundId_Game_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Games_CurrentRoundId",
                table: "Gamble.Games",
                column: "CurrentRoundId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamble.Games_Gamble.Rounds_CurrentRoundId",
                table: "Gamble.Games",
                column: "CurrentRoundId",
                principalTable: "Gamble.Rounds",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamble.Games_Gamble.Rounds_CurrentRoundId",
                table: "Gamble.Games");

            migrationBuilder.DropIndex(
                name: "IX_Gamble.Games_CurrentRoundId",
                table: "Gamble.Games");
        }
    }
}
