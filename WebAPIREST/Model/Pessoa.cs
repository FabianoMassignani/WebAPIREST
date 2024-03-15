using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIREST.Models
{
    [Table("pessoa")]
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_pessoa { get; set; }
        public string nome { get; set; }
        public DateTime data_nascimento { get; set; }
        public bool ativo { get; set; }
        public string cpf { get; set; }
        public string genero { get; set; }
        public string endereco { get; set; }
        public string email { get; set; }
        public DateTime data_atualizacao { get; set; }
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
            this.nome = nome;
            this.data_nascimento = data_nascimento;
            this.ativo = ativo;
            this.cpf = cpf;
            this.genero = genero;
            this.endereco = endereco;
            this.email = email;
            this.data_atualizacao = data_atualizacao;
            Telefones = new List<Telefone>();
        }
    }
}
