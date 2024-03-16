using WebAPIREST.Models;

namespace WebAPIREST.Interfaces
{
    public interface ITelefoneRepository
    {
        List<Telefone> GetAll();
        Telefone GetById(int id);
        ICollection<Pessoa> GetPessoaByTelefone(string id);
        bool TelefoneExist(int id);
        void Post(Telefone telefone);
        void Update(Telefone entity);
        void Delete(int id);
       
    }
}
