using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIREST.Models
{
    [Table("pessoa")]
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_pessoa { get; set; }
        public string Nome { get; set; }
        public DateTime Data_nascimento { get; set; }
        public bool Ativo { get; set; }
        public string Cpf { get; set; }
        public string Genero { get; set; }
        public string Endereco { get; set; }
        public string Email { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public ICollection<Telefone> Telefones { get; set; }

        public Pessoa(
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
            this.Nome = nome;
            this.Data_nascimento = data_nascimento;
            this.Ativo = ativo;
            this.Cpf = cpf;
            this.Genero = genero;
            this.Endereco = endereco;
            this.Email = email;
            this.Data_atualizacao = data_atualizacao;
            Telefones = new List<Telefone>();
        }
    }
}
