namespace WebAPIREST.Dto
{
    public class PessoaDto
    {
        public int id_pessoa { get; private set; }
        public string nome { get; private set; }
        public DateTime data_nascimento { get; private set; }
        public bool ativo { get; private set; }
        public string cpf { get; private set; }
        public string genero { get; private set; }
        public string endereco { get; private set; }
        public string email { get; private set; }
        public DateTime data_atualizacao { get; private set; }
    }
}
