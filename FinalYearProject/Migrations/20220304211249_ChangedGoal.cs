using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalYearProject.Migrations
{
    public partial class ChangedGoal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "goal",
                table: "Question",
                type: "varchar(15)",
                unicode: false,
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(1)",
                oldUnicode: false,
                oldMaxLength: 1,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "goal",
                table: "Question",
                type: "varchar(1)",
                unicode: false,
                maxLength: 1,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(15)",
                oldUnicode: false,
                oldMaxLength: 15,
                oldNullable: true);
        }
    }
}
