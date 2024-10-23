﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class TestMigration4 : Migration
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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
                onDelete: ReferentialAction.Cascade);
        }
    }
}
