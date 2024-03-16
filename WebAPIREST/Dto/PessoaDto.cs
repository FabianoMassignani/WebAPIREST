namespace WebAPIREST.Dto
{
    public class PessoaDto
    {
        public int Id_pessoa { get; private set; }
        public string Nome { get; private set; }
        public DateTime Data_nascimento { get; private set; }
        public bool Ativo { get; private set; }
        public string Cpf { get; private set; }
        public string Genero { get; private set; }
        public string Endereco { get; private set; }
        public string Email { get; private set; }
        public DateTime Data_atualizacao { get; private set; }
        public ICollection<TelefoneDto> Telefones { get; private set; }

        public PessoaDto(
            int id_pessoa,
            string nome,
            DateTime data_nascimento,
            bool ativo,
            string cpf,
            string genero,
            string endereco,
            string email,
            DateTime data_atualizacao,
            ICollection<TelefoneDto> telefones
        )
        {
            this.Id_pessoa = id_pessoa;
            this.Nome = nome;
            this.Data_nascimento = data_nascimento;
            this.Ativo = ativo;
            this.Cpf = cpf;
            this.Genero = genero;
            this.Endereco = endereco;
            this.Email = email;
            this.Data_atualizacao = data_atualizacao;
            this.Telefones = telefones;
        }
    }
}
