using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class course_faculty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Faculty_Id",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_Faculty_Id",
                table: "Course",
                column: "Faculty_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculty_ID",
                table: "Course",
                column: "Faculty_Id",
                principalTable: "Faculty",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculty_ID",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_Faculty_Id",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "Faculty_Id",
                table: "Course");
        }
    }
}
