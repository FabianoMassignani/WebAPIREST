using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace WebAPIREST.Models
{
    [Table("endereco")]
    public class Endereco
    {
        [Key]
        public int Id_endereco { get; set; }

        [Required]
        public string Rua { get; set; }

        [Required]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public string CEP { get; set; }

        public class EnderecoValidator : AbstractValidator<Endereco>
        {
            public EnderecoValidator()
            {
                RuleFor(x => x.Rua).NotEmpty().WithMessage("O campo Rua é obrigatório");

                RuleFor(x => x.Cidade).NotEmpty().WithMessage("O campo Cidade é obrigatório");

                RuleFor(x => x.Estado).NotEmpty().WithMessage("O campo Estado é obrigatório");

                RuleFor(x => x.CEP)
                    .NotEmpty()
                    .WithMessage("O campo CEP é obrigatório")
                    .Length(8)
                    .WithMessage("O campo CEP deve ter 8 caracteres");
            }
        }
    }
}
