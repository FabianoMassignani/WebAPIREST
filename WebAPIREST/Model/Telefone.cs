using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIREST.Models
{
    [Table("telefone")]
    public class Telefone(string tipo, string numero)
    {
        [Key]
        public int Id_telefone { get; set; }
        public string? Tipo { get; set; } = tipo;
        public string? Numero { get; set; } = numero;
        public int PessoaId { get; set; }
        public Pessoa? Pessoa { get; set; }
    }
}
