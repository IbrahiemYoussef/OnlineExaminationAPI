using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class deletedrelationbetweencourseSch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_Schedule",
                table: "Course");

            migrationBuilder.DropIndex(
                name: "IX_Course_schedule_id",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "schedule_id",
                table: "Course");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "schedule_id",
                table: "Course",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Course_schedule_id",
                table: "Course",
                column: "schedule_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_Schedule",
                table: "Course",
                column: "schedule_id",
                principalTable: "Schedule",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
