using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todoApp.Migrations
{
    /// <inheritdoc />
    public partial class added_isCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TodoTask",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "isCompleted",
                table: "TodoTask",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TodoTask");

            migrationBuilder.DropColumn(
                name: "isCompleted",
                table: "TodoTask");
        }
    }
}
