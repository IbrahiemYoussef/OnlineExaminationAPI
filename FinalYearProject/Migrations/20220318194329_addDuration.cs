using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class addDuration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "ExamDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "ExamDetails");
        }
    }
}
