namespace WebAPIREST.ViewModel
{
    public class PessoaViewModel
    {
        public required string Nome { get; set; }
        public DateTime Data_nascimento { get; set; }
        public bool Ativo { get; set; }
        public required string Cpf { get; set; }
        public required string Genero { get; set; }
        public required string Endereco { get; set; }
        public required string Email { get; set; }
        public DateTime Data_atualizacao { get; set; }
    }
}
