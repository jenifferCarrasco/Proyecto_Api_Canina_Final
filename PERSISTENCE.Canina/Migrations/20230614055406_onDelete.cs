using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PERSISTENCE.Canina.Migrations
{
    public partial class onDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caninos_Propietarios_PropietarioId",
                table: "Caninos");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Caninos_CaninoId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Centros_CentroId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Vacunadores_VacunadorId",
                table: "Citas");

            migrationBuilder.AddColumn<Guid>(
                name: "PropietarioId",
                table: "Citas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PropietarioId",
                table: "Citas",
                column: "PropietarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caninos_Propietarios_PropietarioId",
                table: "Caninos",
                column: "PropietarioId",
                principalTable: "Propietarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Caninos_CaninoId",
                table: "Citas",
                column: "CaninoId",
                principalTable: "Caninos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Centros_CentroId",
                table: "Citas",
                column: "CentroId",
                principalTable: "Centros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Propietarios_PropietarioId",
                table: "Citas",
                column: "PropietarioId",
                principalTable: "Propietarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Vacunadores_VacunadorId",
                table: "Citas",
                column: "VacunadorId",
                principalTable: "Vacunadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caninos_Propietarios_PropietarioId",
                table: "Caninos");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Caninos_CaninoId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Centros_CentroId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Propietarios_PropietarioId",
                table: "Citas");

            migrationBuilder.DropForeignKey(
                name: "FK_Citas_Vacunadores_VacunadorId",
                table: "Citas");

            migrationBuilder.DropIndex(
                name: "IX_Citas_PropietarioId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "PropietarioId",
                table: "Citas");

            migrationBuilder.AddForeignKey(
                name: "FK_Caninos_Propietarios_PropietarioId",
                table: "Caninos",
                column: "PropietarioId",
                principalTable: "Propietarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Caninos_CaninoId",
                table: "Citas",
                column: "CaninoId",
                principalTable: "Caninos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Centros_CentroId",
                table: "Citas",
                column: "CentroId",
                principalTable: "Centros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Citas_Vacunadores_VacunadorId",
                table: "Citas",
                column: "VacunadorId",
                principalTable: "Vacunadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
