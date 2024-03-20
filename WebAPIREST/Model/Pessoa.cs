using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace WebAPIREST.Models
{
    [Table("pessoa")]
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_pessoa { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Nome { get; set; }

        public DateTime Data_nascimento { get; set; }
        public bool Ativo { get; set; }
        public string Cpf { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public DateTime Data_cadastro { get; set; }

        // Relacionamento 1 para 1 com Endereco
        public int? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public ICollection<Telefone> Telefones { get; set; }

        public Pessoa()
        {
            Telefones = new List<Telefone>();
        }

        public Pessoa(
            string nome,
            DateTime data_nascimento,
            bool ativo,
            string cpf,
            string genero,
            string email,
            DateTime data_atualizacao,
            DateTime data_cadastro
        )
            : this()
        {
            Nome = nome;
            Data_nascimento = data_nascimento;
            Ativo = ativo;
            Cpf = cpf;
            Genero = genero;
            Email = email;
            Data_atualizacao = data_atualizacao;
            Data_cadastro = data_cadastro;
        }

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
