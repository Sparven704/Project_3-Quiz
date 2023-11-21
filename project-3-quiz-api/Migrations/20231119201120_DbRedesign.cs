using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_3_quiz_api.Migrations
{
    /// <inheritdoc />
    public partial class DbRedesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_ContentTypes_ContentTypeId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Contents_ContentId",
                table: "Questions");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "ContentTypes");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ContentId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ContentTypeId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Timelimit",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "ContentId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ContentTypeId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Questions");

            migrationBuilder.AddColumn<int>(
                name: "TimeLimitMin",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Question",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_Options_OptionModelId",
                table: "Quizzes");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Quizzes_OptionModelId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "OptionModelId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "TimeLimitMin",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Question",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "link",
                table: "Questions");

            migrationBuilder.AddColumn<DateTime>(
                name: "Timelimit",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ContentId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ContentTypeId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Video = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<bool>(type: "bit", nullable: false),
                    IsMultipleAnswers = table.Column<bool>(type: "bit", nullable: false),
                    Video = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ContentId",
                table: "Questions",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ContentTypeId",
                table: "Questions",
                column: "ContentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_ContentTypes_ContentTypeId",
                table: "Questions",
                column: "ContentTypeId",
                principalTable: "ContentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Contents_ContentId",
                table: "Questions",
                column: "ContentId",
                principalTable: "Contents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
