using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication3.Migrations
{
    /// <inheritdoc />
    public partial class MessagesChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Messages",
                newName: "Message");

            migrationBuilder.AddColumn<DateTime>(
                name: "SendDate",
                table: "Messages",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendDate",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Messages",
                newName: "Data");
        }
    }
}
