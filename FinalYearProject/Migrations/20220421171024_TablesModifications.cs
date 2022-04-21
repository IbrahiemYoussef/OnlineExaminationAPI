using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class TablesModifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExamDurationInMinutes",
                table: "ExamDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isExaminated",
                table: "Enrollment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CourseCode",
                table: "Course",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExamDurationInMinutes",
                table: "ExamDetails");

            migrationBuilder.DropColumn(
                name: "isExaminated",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CourseCode",
                table: "Course");
        }
    }
}
