using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class TestMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Players_PlayerTwoId",
                table: "Friends");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Players_PlayerTwoId",
                table: "Friends",
                column: "PlayerTwoId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Players_PlayerTwoId",
                table: "Friends");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Players_PlayerTwoId",
                table: "Friends",
                column: "PlayerTwoId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
