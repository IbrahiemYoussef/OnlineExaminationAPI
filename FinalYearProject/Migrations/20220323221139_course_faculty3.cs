using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class course_faculty3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_ID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Faculty",
                table: "Schedule");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_ID",
                table: "Course",
                column: "Faculty_Id",
                principalTable: "Faculty",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Faculty",
                table: "Schedule",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_ID",
                table: "Course");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Faculty",
                table: "Schedule");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_ID",
                table: "Course",
                column: "Faculty_Id",
                principalTable: "Faculty",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Faculty",
                table: "Schedule",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "id");
        }
    }
}
