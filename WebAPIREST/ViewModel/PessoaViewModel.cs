namespace WebAPIREST.ViewModel
{
    public class PessoaViewModel
    {
        public string nome { get; set; }
        public DateTime data_nascimento { get; set; }
        public bool ativo { get; set; }
        public string cpf { get; set; }
        public string genero { get; set; }
        public string endereco { get; set; }
        public string email { get; set; }
        public DateTime data_atualizacao { get; set; }
    }
}
