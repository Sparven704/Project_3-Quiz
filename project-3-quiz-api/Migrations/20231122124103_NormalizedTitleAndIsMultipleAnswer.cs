using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_3_quiz_api.Migrations
{
    /// <inheritdoc />
    public partial class NormalizedTitleAndIsMultipleAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedTitle",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsMultipleAnswer",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalizedTitle",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "IsMultipleAnswer",
                table: "Questions");

        }
    }
}
