using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace WebAPIREST.Models
{
    [Table("telefone")]
    public class Telefone(string tipo, string numero)
    {
        [Key]
        public int Id_telefone { get; set; }

        [Required]
        public string? Tipo { get; set; } = tipo;

        [Required]
        public string? Numero { get; set; } = numero;
        public int PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }

        public class TelefoneValidator : AbstractValidator<Telefone>
        {
            public TelefoneValidator()
            {
                RuleFor(x => x.Tipo).NotEmpty().WithMessage("O campo Tipo é obrigatório");

                RuleFor(x => x.Numero).NotEmpty().WithMessage("O campo Número é obrigatório");
            }
        }
    }
}
