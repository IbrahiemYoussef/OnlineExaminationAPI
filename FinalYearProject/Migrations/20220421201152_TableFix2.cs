using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class TableFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUser",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "application_user_id",
                table: "Course",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Course_application_user_id",
                table: "Course",
                newName: "IX_Course_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_AspNetUsers_ApplicationUserId",
                table: "Course",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_AspNetUsers_ApplicationUserId",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Course",
                newName: "application_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Course_ApplicationUserId",
                table: "Course",
                newName: "IX_Course_application_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser",
                table: "Course",
                column: "application_user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
