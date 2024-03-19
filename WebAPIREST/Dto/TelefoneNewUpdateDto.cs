using System.ComponentModel.DataAnnotations;

namespace WebAPIREST.Dto
{
    public class TelefoneNewUpdateDto
    {
        public required string Tipo { get; set; }
        public required string Numero { get; set; }
    }
}
