using System.ComponentModel.DataAnnotations;
using WebAPIREST.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPIREST.Dto
{
    public class TelefoneNewDto
    {
        public required string Tipo { get; set; }
        public required string Numero { get; set; }
    }
}
