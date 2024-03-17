using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIREST.Models
{
    [Table("pessoa")]
    public class Pessoa(
        string nome,
        DateTime data_nascimento,
        bool ativo,
        string cpf,
        string genero,
        string endereco,
        string email,
        DateTime data_atualizacao
        )
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_pessoa { get; set; }
        public string Nome { get; set; } = nome;
        public DateTime Data_nascimento { get; set; } = data_nascimento;
        public bool Ativo { get; set; } = ativo;
        public string Cpf { get; set; } = cpf;
        public string Genero { get; set; } = genero;
        public string Endereco { get; set; } = endereco;
        public string Email { get; set; } = email;
        public DateTime Data_atualizacao { get; set; } = data_atualizacao;
        public ICollection<Telefone> Telefones { get; set; } = [];
    }
}
