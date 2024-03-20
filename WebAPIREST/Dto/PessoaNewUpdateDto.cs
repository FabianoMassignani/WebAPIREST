using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPIREST.Dto
{
    public class PessoaNewUpdateDto
    {
        public string Nome { get; set; }
        public DateTime Data_nascimento { get; set; }
        public bool Ativo { get; set; }
        public string Cpf { get; set; }
        public string Genero { get; set; }
        public string Email { get; set; }
        public DateTime Data_atualizacao { get; set; }
        public DateTime Data_cadastro { get; set; }

        // Informações do endereço
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
    }
}
