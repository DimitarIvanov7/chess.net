using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class changeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_GameTypes_gameTypeId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Player_blackPlayerId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Player_whitePlayerId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Player_winnerId",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameIndex(
                name: "IX_Game_winnerId",
                table: "Games",
                newName: "IX_Games_winnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Game_whitePlayerId",
                table: "Games",
                newName: "IX_Games_whitePlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Game_gameTypeId",
                table: "Games",
                newName: "IX_Games_gameTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Game_blackPlayerId",
                table: "Games",
                newName: "IX_Games_blackPlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_GameTypes_gameTypeId",
                table: "Games",
                column: "gameTypeId",
                principalTable: "GameTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_blackPlayerId",
                table: "Games",
                column: "blackPlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_whitePlayerId",
                table: "Games",
                column: "whitePlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Players_winnerId",
                table: "Games",
                column: "winnerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_GameTypes_gameTypeId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_blackPlayerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_whitePlayerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Players_winnerId",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameIndex(
                name: "IX_Games_winnerId",
                table: "Game",
                newName: "IX_Game_winnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_whitePlayerId",
                table: "Game",
                newName: "IX_Game_whitePlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_gameTypeId",
                table: "Game",
                newName: "IX_Game_gameTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_blackPlayerId",
                table: "Game",
                newName: "IX_Game_blackPlayerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_GameTypes_gameTypeId",
                table: "Game",
                column: "gameTypeId",
                principalTable: "GameTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Player_blackPlayerId",
                table: "Game",
                column: "blackPlayerId",
                principalTable: "Player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Player_whitePlayerId",
                table: "Game",
                column: "whitePlayerId",
                principalTable: "Player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Player_winnerId",
                table: "Game",
                column: "winnerId",
                principalTable: "Player",
                principalColumn: "Id");
        }
    }
}
