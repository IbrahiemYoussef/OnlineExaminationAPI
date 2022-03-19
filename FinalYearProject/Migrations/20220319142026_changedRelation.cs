using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class changedRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedule_Faculty_FacultyId",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_FacultyId",
                table: "Schedule");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_FacultyId",
                table: "Schedule",
                column: "FacultyId");

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
                name: "FK_Schedule_Faculty",
                table: "Schedule");

            migrationBuilder.DropIndex(
                name: "IX_Schedule_FacultyId",
                table: "Schedule");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_FacultyId",
                table: "Schedule",
                column: "FacultyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedule_Faculty_FacultyId",
                table: "Schedule",
                column: "FacultyId",
                principalTable: "Faculty",
                principalColumn: "id");
        }
    }
}
