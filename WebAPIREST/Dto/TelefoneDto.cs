using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WebAPIREST.Models;

namespace WebAPIREST.Dto
{
    public class TelefoneDto
    {
        public int Id_telefone { get; set; }
        public required string Tipo { get; set; }
        public required string Numero { get; set; }
        public int PessoaId { get; set; }
    }
}
