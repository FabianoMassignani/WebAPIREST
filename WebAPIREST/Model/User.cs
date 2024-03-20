using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;

namespace WebAPIREST.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        public int Id_user { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; }

        public class UserValidator : AbstractValidator<User>
        {
            public UserValidator()
            {
                RuleFor(x => x.Username)
                    .NotEmpty()
                    .WithMessage("O campo Username é obrigatório")
                    .Length(5, 100)
                    .WithMessage("O campo Username deve ter entre 5 e 100 caracteres");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .WithMessage("O campo Password é obrigatório")
                    .Length(5, 100)
                    .WithMessage("O campo Password deve ter entre 5 e 100 caracteres");

                RuleFor(x => x.Role).NotEmpty().WithMessage("O campo Role é obrigatório");
            }
        }
    }
}
