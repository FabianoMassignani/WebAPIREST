using System.ComponentModel.DataAnnotations;

namespace WebAPIREST.Dto
{
    public class TelefoneNewUpdateDto
    {
        public int Id_telefone { get; set; }
        public required string Tipo { get; set; }
        public required string Numero { get; set; }
        public int PessoaId { get; set; }
    }
}
