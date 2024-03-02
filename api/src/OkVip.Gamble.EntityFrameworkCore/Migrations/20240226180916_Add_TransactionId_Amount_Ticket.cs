using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class Add_TransactionId_Amount_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Gamble.Tickets",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "Gamble.Tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Gamble.Tickets_TransactionId",
                table: "Gamble.Tickets",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Transactions_TransactionId",
                table: "Gamble.Tickets",
                column: "TransactionId",
                principalTable: "Gamble.Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Transactions_TransactionId",
                table: "Gamble.Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Gamble.Tickets_TransactionId",
                table: "Gamble.Tickets");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Gamble.Tickets");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Gamble.Tickets");
        }
    }
}
