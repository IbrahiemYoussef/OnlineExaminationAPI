using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class EnrollementFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnrolementProfessor",
                columns: table => new
                {
                    course_id = table.Column<int>(type: "int", nullable: false),
                    application_user_id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolementProfessor", x => new { x.course_id, x.application_user_id });
                    table.ForeignKey(
                        name: "FK_Enrollment_CourseProf",
                        column: x => x.course_id,
                        principalTable: "Course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Enrollment_Professor",
                        column: x => x.application_user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolementProfessor_application_user_id",
                table: "EnrolementProfessor",
                column: "application_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrolementProfessor");
        }
    }
}
