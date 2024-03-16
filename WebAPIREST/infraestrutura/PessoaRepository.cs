using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIREST.infraestrutura
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        [ProducesResponseType(200, Type = typeof(IEnumerable<Pessoa>))]
        public List<Pessoa> GetAll()
        {
            try
            {
                return _context.Pessoas.Include(p => p.Telefones).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todas as pessoas.", ex);
            }
        }

        public Pessoa GetById(int id)
        {
            try
            {
                return _context.Pessoas.FirstOrDefault(p => p.Id_pessoa == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a pessoa por ID.", ex);
            }
        }

        public Pessoa GetByNome(string nome)
        {
            try
            {
                return _context.Pessoas.FirstOrDefault(p => p.Nome == nome);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a pessoa por nome.", ex);
            }
        }

        public Pessoa GetByCPF(string cpf)
        {
            try
            {
                return _context.Pessoas.FirstOrDefault(p => p.Cpf == cpf);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a pessoa por CPF.", ex);
            }
        }

        public bool PessoaExist(int id)
        {
            try
            {
                return _context.Pessoas.Any(p => p.Id_pessoa == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao verificar existência da pessoa.", ex);
            }
        }

        public void Post(Pessoa pessoa)
        {
            try
            {
                _context.Pessoas.Add(pessoa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar pessoa.", ex);
            }
        }

        public void Update(Pessoa pessoa)
        {
            try
            {
                if (_context.Entry(pessoa).State == EntityState.Detached)
                {
                    _context.Attach(pessoa);
                }

                _context.Entry(pessoa).State = EntityState.Modified;

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Erro de concorrência ao atualizar pessoa.");
            }
            catch (DbUpdateException)
            {
                throw new Exception("Erro ao atualizar pessoa no banco de dados.");
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar pessoa.", ex);
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
