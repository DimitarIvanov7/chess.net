using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class TestMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Players_PlayerOneId",
                table: "Friends");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Players_PlayerOneId",
                table: "Friends",
                column: "PlayerOneId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Players_PlayerOneId",
                table: "Friends");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Players_PlayerOneId",
                table: "Friends",
                column: "PlayerOneId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
