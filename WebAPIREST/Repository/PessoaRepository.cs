using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIREST.infraestrutura;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;

namespace WebAPIREST.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        [ProducesResponseType(200, Type = typeof(IEnumerable<Pessoa>))]
        public List<Pessoa> GetAllPessoas()
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

        public Pessoa GetPessoaById(int id)
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

        public Pessoa GetPessoaByName(string nome)
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

        public Pessoa GetPessoaByCPF(string cpf)
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

        public bool CreatePessoa(Pessoa pessoa)
        {
            try
            {
                _context.Pessoas.Add(pessoa);
                return Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao adicionar pessoa.", ex);
            }
        }


        public bool UpdatePessoa(Pessoa pessoa)
        {
            try
            {
                if (_context.Entry(pessoa).State == EntityState.Detached)
                {
                    _context.Attach(pessoa);
                }

                _context.Entry(pessoa).State = EntityState.Modified;

                return Save();
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

        public bool DeletePessoa(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeletePessoa(Pessoa pessoa)
        {
            try
            {
                _context.Remove(pessoa);
                return Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar pessoa.", ex);
            }
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0 ? true : false;
        }
    }
}
