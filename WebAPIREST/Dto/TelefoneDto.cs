using System.ComponentModel.DataAnnotations;
using WebAPIREST.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
