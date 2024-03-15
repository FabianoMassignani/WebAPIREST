using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;

namespace WebAPIREST.infraestrutura
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        [ProducesResponseType(200, Type = typeof(IEnumerable<Pessoa>))]
        public List<Pessoa> GetAll()
        {
            return _context.Pessoas
                .Include(p => p.Telefones)
                .OrderBy(p => p.id_pessoa)
                .ToList();
        }

        public Pessoa GetById(int id)
        {
            return _context.Pessoas.Where(p => p.id_pessoa == id).FirstOrDefault();
        }

        public Pessoa GetByNome(string nome)
        {
            return _context.Pessoas.Where(p => p.nome == nome).FirstOrDefault();
        }

        public Pessoa GetByCPF(string cpf)
        {
            return _context.Pessoas.Where(p => p.cpf == cpf).FirstOrDefault();
        }

        public void Add(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
        }

        public bool PessoaExist(int id)
        {
            return _context.Pessoas.Any(p => p.id_pessoa == id);
        }
    }
}
