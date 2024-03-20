using System.ComponentModel.DataAnnotations;
using WebAPIREST.Models;

namespace WebAPIREST.Dto
{
    public class PessoaDto()
    {
        public int Id_pessoa { get; private set; }
        public string Nome { get; private set; }
        public DateTime Data_nascimento { get; private set; }
        public bool Ativo { get; private set; }
        public string Cpf { get; private set; }
        public string Genero { get; private set; }
        public string Email { get; private set; }
        public DateTime Data_atualizacao { get; private set; }
        public DateTime Data_cadastro { get; private set; }
        public Endereco Endereco { get; set; }
        public ICollection<TelefoneDto> Telefones { get; private set; }
    }
}
