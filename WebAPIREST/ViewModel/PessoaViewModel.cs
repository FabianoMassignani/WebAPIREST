using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPIREST.ViewModel
{
    public class PessoaViewModel
    {
        public int Id_pessoa { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public required string Nome { get; set; }

        public DateTime Data_nascimento { get; set; }
        public bool Ativo { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public required string Cpf { get; set; }

        [Required(ErrorMessage = "O campo Gênero é obrigatório.")]
        public required string Genero { get; set; }

        [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
        public required string Endereco { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public required string Email { get; set; }

        public DateTime Data_atualizacao { get; set; }
    }
}
