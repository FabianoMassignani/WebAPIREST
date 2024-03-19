using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace WebAPIREST.Models
{
    [Table("pessoa")]
    public class Pessoa(
        string nome,
        DateTime data_nascimento,
        bool ativo,
        string cpf,
        string genero,
        string endereco,
        string email,
        DateTime data_atualizacao,
        DateTime data_cadastro
    )
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_pessoa { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Nome { get; set; } = nome;
        public DateTime Data_nascimento { get; set; } = data_nascimento;
        public bool Ativo { get; set; } = ativo;
        public string Cpf { get; set; } = cpf;
        public string Genero { get; set; } = genero;
        public string Endereco { get; set; } = endereco;
        public string Email { get; set; } = email;
        public DateTime Data_atualizacao { get; set; } = data_atualizacao;
        public DateTime Data_cadastro { get; set; } = data_cadastro;
        public ICollection<Telefone> Telefones { get; set; } = [];

        public class PessoaValidator : AbstractValidator<Pessoa>
        {
            public PessoaValidator()
            {
                RuleFor(x => x.Nome)
                    .NotEmpty()
                    .WithMessage("Informe o nome do cliente")
                    .Length(5, 100)
                    .WithMessage("O nome deverá ter entre 5 a 100 caracteres");
            }
        }
    }
}
