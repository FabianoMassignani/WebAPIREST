using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIREST.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_telefone_pessoa_Pessoaid_pessoa",
                table: "telefone");

            migrationBuilder.RenameColumn(
                name: "Pessoaid_pessoa",
                table: "telefone",
                newName: "PessoaId");

            migrationBuilder.RenameIndex(
                name: "IX_telefone_Pessoaid_pessoa",
                table: "telefone",
                newName: "IX_telefone_PessoaId");

            migrationBuilder.AddForeignKey(
                name: "FK_telefone_pessoa_PessoaId",
                table: "telefone",
                column: "PessoaId",
                principalTable: "pessoa",
                principalColumn: "id_pessoa",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_telefone_pessoa_PessoaId",
                table: "telefone");

            migrationBuilder.RenameColumn(
                name: "PessoaId",
                table: "telefone",
                newName: "Pessoaid_pessoa");

            migrationBuilder.RenameIndex(
                name: "IX_telefone_PessoaId",
                table: "telefone",
                newName: "IX_telefone_Pessoaid_pessoa");

            migrationBuilder.AddForeignKey(
                name: "FK_telefone_pessoa_Pessoaid_pessoa",
                table: "telefone",
                column: "Pessoaid_pessoa",
                principalTable: "pessoa",
                principalColumn: "id_pessoa",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
