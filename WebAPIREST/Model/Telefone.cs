using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIREST.Models
{
    [Table("telefone")]
    public class Telefone
    {
        [Key]
        public int Id_telefone { get; set; }
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }

        public Telefone(string tipo, string numero)
        {
            this.Tipo = tipo;
            this.Numero = numero;
        }
    }
}
