using Microsoft.EntityFrameworkCore.Migrations;

namespace ConstructionApp.Migrations
{
    public partial class Update_MacEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Tuoi",
                table: "MAC",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "DoSut",
                table: "MAC",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Tuoi",
                table: "MAC",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<string>(
                name: "DoSut",
                table: "MAC",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
