using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;

namespace WebAPIREST.infraestrutura
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public List<Telefone> GetAll()
        {
            return _context.Telefones.OrderBy(p => p.id_telefone).ToList();
        }

        public Telefone GetById(int id)
        {
            return _context.Telefones.Where(p => p.id_telefone == id).FirstOrDefault();
        }

        public ICollection<Pessoa> GetPessoaByTelefone(string id)
        {
            return _context.Pessoas
                .Include(p => p.Telefones)
                .Where(p => p.Telefones.Any(t => t.numero == id))
                .ToList();
        }

        public void Add(Telefone telefone)
        {
            _context.Telefones.Add(telefone);
            _context.SaveChanges();
        }
    }
}
