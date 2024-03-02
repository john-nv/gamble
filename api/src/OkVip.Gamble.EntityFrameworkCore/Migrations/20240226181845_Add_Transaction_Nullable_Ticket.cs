using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OkVip.Gamble.Migrations
{
    /// <inheritdoc />
    public partial class Add_Transaction_Nullable_Ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Transactions_TransactionId",
                table: "Gamble.Tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Gamble.Tickets",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Transactions_TransactionId",
                table: "Gamble.Tickets",
                column: "TransactionId",
                principalTable: "Gamble.Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Transactions_TransactionId",
                table: "Gamble.Tickets");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "Gamble.Tickets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Gamble.Tickets_Gamble.Transactions_TransactionId",
                table: "Gamble.Tickets",
                column: "TransactionId",
                principalTable: "Gamble.Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
