using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class RenameTablesSingular : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    whitePlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    blackPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    winnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    gameTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    gameTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Game_GameTypes_gameTypeId",
                        column: x => x.gameTypeId,
                        principalTable: "GameTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Game_Player_blackPlayerId",
                        column: x => x.blackPlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Game_Player_whitePlayerId",
                        column: x => x.whitePlayerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Game_Player_winnerId",
                        column: x => x.winnerId,
                        principalTable: "Player",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_blackPlayerId",
                table: "Game",
                column: "blackPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_gameTypeId",
                table: "Game",
                column: "gameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_whitePlayerId",
                table: "Game",
                column: "whitePlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_winnerId",
                table: "Game",
                column: "winnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    blackPlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    gameTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    whitePlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    winnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    gameTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_GameTypes_gameTypeId",
                        column: x => x.gameTypeId,
                        principalTable: "GameTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Games_Players_blackPlayerId",
                        column: x => x.blackPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Players_whitePlayerId",
                        column: x => x.whitePlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Games_Players_winnerId",
                        column: x => x.winnerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_blackPlayerId",
                table: "Games",
                column: "blackPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_gameTypeId",
                table: "Games",
                column: "gameTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_whitePlayerId",
                table: "Games",
                column: "whitePlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_winnerId",
                table: "Games",
                column: "winnerId");
        }
    }
}
