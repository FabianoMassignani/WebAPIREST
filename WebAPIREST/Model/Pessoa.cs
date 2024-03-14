using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIREST.Models
{
    [Table("pessoa")]

    public class Pessoa
    {
        [Key]
        public int id_pessoa { get; private set; } 
        public  string nome { get; private set; }
        public DateTime data_nascimento { get; private set; }
        public bool ativo { get; private set; }
        public string cpf { get; private set; }
        public string genero { get; private set; }
        public string endereco { get; private set; }
        public  string email { get; private set; }
        public DateTime data_atualizacao { get; private set; }

        public Pessoa(string nome, DateTime data_nascimento,
            bool ativo, string cpf, string genero, string endereco,
            string email, DateTime data_atualizacao)
        {       

            this.nome = nome;
            this.data_nascimento = data_nascimento;
            this.ativo = ativo;
            this.cpf = cpf;
            this.genero = genero;
            this.endereco = endereco;
            this.email = email;
            this.data_atualizacao = data_atualizacao;
        }
    }
  
}
