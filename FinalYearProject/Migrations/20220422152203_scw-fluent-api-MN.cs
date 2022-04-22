using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class scwfluentapiMN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleWithCourse_Course_course_id",
                table: "ScheduleWithCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleWithCourse_Schedule_schedule_id",
                table: "ScheduleWithCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleWithCourse",
                table: "ScheduleWithCourse");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleWithCourse_course_id",
                table: "ScheduleWithCourse");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleWithCourse_schedule_id",
                table: "ScheduleWithCourse");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ScheduleWithCourse");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleWithCourse",
                table: "ScheduleWithCourse",
                columns: new[] { "schedule_id", "course_id" });

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleWithCourse_course_id",
                table: "ScheduleWithCourse",
                column: "course_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SWC_Course",
                table: "ScheduleWithCourse",
                column: "course_id",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SWC_Schedule",
                table: "ScheduleWithCourse",
                column: "schedule_id",
                principalTable: "Schedule",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SWC_Course",
                table: "ScheduleWithCourse");

            migrationBuilder.DropForeignKey(
                name: "FK_SWC_Schedule",
                table: "ScheduleWithCourse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ScheduleWithCourse",
                table: "ScheduleWithCourse");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleWithCourse_course_id",
                table: "ScheduleWithCourse");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ScheduleWithCourse",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ScheduleWithCourse",
                table: "ScheduleWithCourse",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleWithCourse_course_id",
                table: "ScheduleWithCourse",
                column: "course_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleWithCourse_schedule_id",
                table: "ScheduleWithCourse",
                column: "schedule_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleWithCourse_Course_course_id",
                table: "ScheduleWithCourse",
                column: "course_id",
                principalTable: "Course",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleWithCourse_Schedule_schedule_id",
                table: "ScheduleWithCourse",
                column: "schedule_id",
                principalTable: "Schedule",
                principalColumn: "id");
        }
    }
}
