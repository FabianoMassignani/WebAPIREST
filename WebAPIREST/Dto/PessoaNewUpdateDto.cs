using System.ComponentModel.DataAnnotations;

namespace WebAPIREST.Dto
{
    public class PessoaNewUpdateDto(
        string nome,
        DateTime data_nascimento,
        bool ativo,
        string cpf,
        string genero,
        string endereco,
        string email
    )
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; private set; } = nome;
        public DateTime Data_nascimento { get; private set; } = data_nascimento;
        public bool Ativo { get; private set; } = ativo;
        public string Cpf { get; private set; } = cpf;
        public string Genero { get; private set; } = genero;
        public string Endereco { get; private set; } = endereco;

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; private set; } = email;
    }
}
