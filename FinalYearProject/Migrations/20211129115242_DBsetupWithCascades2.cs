using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class DBsetupWithCascades2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Exam",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Question",
                table: "ExamQuestions");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Exam",
                table: "ExamQuestions",
                column: "exam_id",
                principalTable: "Exam",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Question",
                table: "ExamQuestions",
                column: "question_id",
                principalTable: "Question",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Exam",
                table: "ExamQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_ExamQuestions_Question",
                table: "ExamQuestions");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Exam",
                table: "ExamQuestions",
                column: "exam_id",
                principalTable: "Exam",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamQuestions_Question",
                table: "ExamQuestions",
                column: "question_id",
                principalTable: "Question",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
