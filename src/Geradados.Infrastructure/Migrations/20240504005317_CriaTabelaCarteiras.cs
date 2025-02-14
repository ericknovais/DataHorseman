using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geradados.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelaCarteiras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carteiras",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaID = table.Column<int>(type: "int", nullable: false),
                    AtivoID = table.Column<int>(type: "int", nullable: false),
                    Cota = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carteiras", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Carteiras_Ativos_AtivoID",
                        column: x => x.AtivoID,
                        principalTable: "Ativos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carteiras_Pessoas_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_AtivoID",
                table: "Carteiras",
                column: "AtivoID");

            migrationBuilder.CreateIndex(
                name: "IX_Carteiras_PessoaID",
                table: "Carteiras",
                column: "PessoaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carteiras");
        }
    }
}
