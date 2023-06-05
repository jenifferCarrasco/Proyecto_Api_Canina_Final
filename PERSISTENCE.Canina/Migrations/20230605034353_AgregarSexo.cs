using Microsoft.EntityFrameworkCore.Migrations;

namespace PERSISTENCE.Canina.Migrations
{
    public partial class AgregarSexo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sexo",
                table: "Vacunadores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sexo",
                table: "Propietarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Sexo",
                table: "Caninos",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Vacunadores");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Propietarios");

            migrationBuilder.AlterColumn<bool>(
                name: "Sexo",
                table: "Caninos",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
