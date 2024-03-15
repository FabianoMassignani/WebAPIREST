using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIREST.Models
{
    [Table("telefone")]
    public class Telefone
    {
        [Key]
        public int id_telefone { get; set; }
        public string tipo { get; set; }
        public string numero { get; set; }
        public int id_pessoa { get; set; }
        public Pessoa Pessoa { get; set; }
        public Telefone(string tipo, string numero)
        {
            this.tipo = tipo;
            this.numero = numero;
        }
    }
}
