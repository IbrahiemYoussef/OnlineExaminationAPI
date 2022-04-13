using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class BackTypeOfquestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfMultipleMCQ",
                table: "ExamDetails");

            migrationBuilder.DropColumn(
                name: "NumberOfSingleMCQ",
                table: "ExamDetails");

            migrationBuilder.DropColumn(
                name: "NumberOfTrueFalse",
                table: "ExamDetails");

            migrationBuilder.DropColumn(
                name: "NumberOfWritten",
                table: "ExamDetails");

            migrationBuilder.AddColumn<string>(
                name: "TypeOfQuestions",
                table: "ExamDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeOfQuestions",
                table: "ExamDetails");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfMultipleMCQ",
                table: "ExamDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSingleMCQ",
                table: "ExamDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfTrueFalse",
                table: "ExamDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfWritten",
                table: "ExamDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
