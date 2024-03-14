using WebAPIREST.Models;

namespace WebAPIREST.Model
{
    public interface IPessoaRepository
    {
        List<Pessoa> Get();

        void Add(Pessoa pessoa);

        void update(Pessoa pessoa);

        void delete(int id);
    }
}
