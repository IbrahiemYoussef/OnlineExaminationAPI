using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class addedExamDetailss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamDetails_Course_CoursesId",
                table: "ExamDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExamDetails_CoursesId",
                table: "ExamDetails");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "ExamDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ExamDetails_Course_id",
                table: "ExamDetails",
                column: "Course_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamDetails_Course_Course_id",
                table: "ExamDetails",
                column: "Course_id",
                principalTable: "Course",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamDetails_Course_Course_id",
                table: "ExamDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExamDetails_Course_id",
                table: "ExamDetails");

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "ExamDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamDetails_CoursesId",
                table: "ExamDetails",
                column: "CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamDetails_Course_CoursesId",
                table: "ExamDetails",
                column: "CoursesId",
                principalTable: "Course",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
