using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataHorseman.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CriaIndexCPFPesso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ativos_TipoDeAtivos_TipoDeAtivoID",
                table: "Ativos");

            migrationBuilder.RenameColumn(
                name: "TipoDeAtivoID",
                table: "Ativos",
                newName: "TipoDeAtivoId");

            migrationBuilder.RenameIndex(
                name: "IX_Ativos_TipoDeAtivoID",
                table: "Ativos",
                newName: "IX_Ativos_TipoDeAtivoId");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Pessoas",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CPF",
                table: "Pessoas",
                column: "CPF",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Ativos_TipoDeAtivos_TipoDeAtivoId",
                table: "Ativos",
                column: "TipoDeAtivoId",
                principalTable: "TipoDeAtivos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ativos_TipoDeAtivos_TipoDeAtivoId",
                table: "Ativos");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_CPF",
                table: "Pessoas");

            migrationBuilder.RenameColumn(
                name: "TipoDeAtivoId",
                table: "Ativos",
                newName: "TipoDeAtivoID");

            migrationBuilder.RenameIndex(
                name: "IX_Ativos_TipoDeAtivoId",
                table: "Ativos",
                newName: "IX_Ativos_TipoDeAtivoID");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Pessoas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Ativos_TipoDeAtivos_TipoDeAtivoID",
                table: "Ativos",
                column: "TipoDeAtivoID",
                principalTable: "TipoDeAtivos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
