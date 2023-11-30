using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_3_quiz_api.Migrations
{
    /// <inheritdoc />
    public partial class MediaFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Questions_QuestionId",
                table: "Media");

            migrationBuilder.DropIndex(
                name: "IX_Media_QuestionId",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Media");

            migrationBuilder.AddColumn<Guid>(
                name: "MediaId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_MediaId",
                table: "Questions",
                column: "MediaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Media_MediaId",
                table: "Questions",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Media_MediaId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_MediaId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "Questions");

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "Media",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Media_QuestionId",
                table: "Media",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Questions_QuestionId",
                table: "Media",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
