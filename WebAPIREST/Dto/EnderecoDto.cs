using System.ComponentModel.DataAnnotations;

namespace WebAPIREST.Dto
{
    public class EnderecoDto
    {
        public int Id_endereco { get; set; }
        public required string Rua { get; set; }

        [Required(ErrorMessage = "O número é obrigatório.")]
        public required string Numero { get; set; }

        public required string Complemento { get; set; }

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        public required string Cidade { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        public required string Estado { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "Formato de CEP inválido. Use o formato: 12345-678.")]
        public required string CEP { get; set; }
    }
}
