using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class course_faculty2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Faculty",
                table: "Schedule");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Faculty",
                table: "Schedule",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Faculty",
                table: "Schedule");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Faculty",
                table: "Schedule",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
