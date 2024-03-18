using System.ComponentModel.DataAnnotations;

namespace WebAPIREST.Dto
{
    public class PessoaDto(
        int id_pessoa,
        string nome,
        DateTime data_nascimento,
        bool ativo,
        string cpf,
        string genero,
        string endereco,
        string email,
        DateTime data_atualizacao,
        DateTime data_cadastro,
        ICollection<TelefoneDto> telefones
    )
    {
        public int Id_pessoa { get; private set; } = id_pessoa;

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; private set; } = nome;
        public DateTime Data_nascimento { get; private set; } = data_nascimento;
        public bool Ativo { get; private set; } = ativo;
        public string Cpf { get; private set; } = cpf;
        public string Genero { get; private set; } = genero;
        public string Endereco { get; private set; } = endereco;

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; private set; } = email;
        public DateTime Data_atualizacao { get; private set; } = data_atualizacao;
        public DateTime Data_cadastro { get; private set; } = data_cadastro;
        public ICollection<TelefoneDto> Telefones { get; private set; } = telefones;
    }
}
