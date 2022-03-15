using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class deletedClassStudentAnsandExamQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAnswer");

            migrationBuilder.DropTable(
                name: "ExamQuestions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExamQuestions",
                columns: table => new
                {
                    exam_id = table.Column<int>(type: "int", nullable: false),
                    question_id = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamQuestions_1", x => new { x.exam_id, x.question_id });
                    table.UniqueConstraint("AK_ExamQuestions_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_ExamQuestions_Question",
                        column: x => x.question_id,
                        principalTable: "Question",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "StudentAnswer",
                columns: table => new
                {
                    application_user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    exam_questions_id = table.Column<int>(type: "int", nullable: false),
                    answer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAnswer", x => new { x.application_user_id, x.exam_questions_id });
                    table.ForeignKey(
                        name: "FK_StudentAnswer_ApplicationUsers",
                        column: x => x.application_user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentAnswer_ExamQuestions",
                        column: x => x.exam_questions_id,
                        principalTable: "ExamQuestions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExamQuestions_question_id",
                table: "ExamQuestions",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "unique_eqa_id",
                table: "ExamQuestions",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAnswer_exam_questions_id",
                table: "StudentAnswer",
                column: "exam_questions_id");
        }
    }
}
