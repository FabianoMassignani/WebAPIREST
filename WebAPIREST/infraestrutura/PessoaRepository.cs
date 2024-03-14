using WebAPIREST.Model;
using WebAPIREST.Models;

namespace WebAPIREST.infraestrutura
{
    public class PessoaRepository : IPessoaRepository

    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Add(Pessoa pessoa)
        {
             _context.Pessoas.Add(pessoa);
             _context.SaveChanges();
        }   

        public void delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pessoa> Get()
        {
           return _context.Pessoas.ToList();
        }

        public void update(Pessoa pessoa)
        {
            throw new NotImplementedException();
        }
    }
}
