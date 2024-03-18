using WebAPIREST.Models;

namespace WebAPIREST.Interfaces
{
    public interface ITelefoneRepository
    {
        List<Telefone> GetAllTelefone();
        Telefone GetTelefoneById(int id);
        bool TelefoneExist(int id);
        bool CreateTelefone(Telefone Telefone);
        bool UpdateTelefone(Telefone Telefone);
        bool DeleteTelefone(Telefone Telefone);
        bool Save();
    }
}
