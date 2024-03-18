using Microsoft.EntityFrameworkCore;
using WebAPIREST.infraestrutura;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;

namespace WebAPIREST.Repository
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly ConnectionContext _context = new();

        public List<Telefone> GetAllTelefone()
        {
            try
            {
                return [.. _context.Telefones.OrderBy(p => p.Id_telefone)];
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todos os telefones.", ex);
            }
        }

        public Telefone GetTelefoneById(int id)
        {
            try
            {
                return _context.Telefones.FirstOrDefault(p => p.Id_telefone == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter o telefone por ID.", ex);
            }
        }

        public bool CreateTelefone(Telefone telefone)
        {
            try
            {
                _context.Telefones.Add(telefone);
                return Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar telefone.", ex);
            }
        }

        public bool TelefoneExist(int id)
        {
            try
            {
                return _context.Telefones.Any(p => p.Id_telefone == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar existência do telefone.", ex);
            }
        }

        public bool UpdateTelefone(Telefone telefone)
        {
            try
            {
                if (_context.Entry(telefone).State == EntityState.Detached)
                {
                    _context.Attach(telefone);
                }

                _context.Entry(telefone).State = EntityState.Modified;

                return Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Erro de concorrência ao atualizar telefone.");
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro ao atualizar telefone no banco de dados.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar telefone.", ex);
            }
        }

        public bool DeleteTelefone(Telefone telefone)
        {
            try
            {
                _context.Remove(telefone);
                return Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar telefone.", ex);
            }
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
