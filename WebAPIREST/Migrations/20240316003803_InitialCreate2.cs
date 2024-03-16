using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPIREST.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tipo",
                table: "telefone",
                newName: "Tipo");

            migrationBuilder.RenameColumn(
                name: "numero",
                table: "telefone",
                newName: "Numero");

            migrationBuilder.RenameColumn(
                name: "id_telefone",
                table: "telefone",
                newName: "Id_telefone");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "pessoa",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "genero",
                table: "pessoa",
                newName: "Genero");

            migrationBuilder.RenameColumn(
                name: "endereco",
                table: "pessoa",
                newName: "Endereco");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "pessoa",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "data_nascimento",
                table: "pessoa",
                newName: "Data_nascimento");

            migrationBuilder.RenameColumn(
                name: "data_atualizacao",
                table: "pessoa",
                newName: "Data_atualizacao");

            migrationBuilder.RenameColumn(
                name: "cpf",
                table: "pessoa",
                newName: "Cpf");

            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "pessoa",
                newName: "Ativo");

            migrationBuilder.RenameColumn(
                name: "id_pessoa",
                table: "pessoa",
                newName: "Id_pessoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "telefone",
                newName: "tipo");

            migrationBuilder.RenameColumn(
                name: "Numero",
                table: "telefone",
                newName: "numero");

            migrationBuilder.RenameColumn(
                name: "Id_telefone",
                table: "telefone",
                newName: "id_telefone");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "pessoa",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Genero",
                table: "pessoa",
                newName: "genero");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "pessoa",
                newName: "endereco");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "pessoa",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "Data_nascimento",
                table: "pessoa",
                newName: "data_nascimento");

            migrationBuilder.RenameColumn(
                name: "Data_atualizacao",
                table: "pessoa",
                newName: "data_atualizacao");

            migrationBuilder.RenameColumn(
                name: "Cpf",
                table: "pessoa",
                newName: "cpf");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "pessoa",
                newName: "ativo");

            migrationBuilder.RenameColumn(
                name: "Id_pessoa",
                table: "pessoa",
                newName: "id_pessoa");
        }
    }
}
