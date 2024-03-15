using WebAPIREST.Models;

namespace WebAPIREST.Interfaces
{
    public interface ITelefoneRepository
    {
        List<Telefone> GetAll();
        Telefone GetById(int id);
        ICollection<Pessoa> GetPessoaByTelefone(string id);
        void Add(Telefone telefone);
    }
}
