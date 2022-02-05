using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class SomeModifedinExamDetailsClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamDetails_Course_id",
                table: "ExamDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ExamDetails_Course_id",
                table: "ExamDetails",
                column: "Course_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExamDetails_Course_id",
                table: "ExamDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ExamDetails_Course_id",
                table: "ExamDetails",
                column: "Course_id");
        }
    }
}
