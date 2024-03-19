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
    public class PessoaRepository : IUserRepository
    {
        private readonly ConnectionContext _context = new();

        [ProducesResponseType(200, Type = typeof(IEnumerable<Pessoa>))]
        public List<Pessoa> GetAllPessoas()
        {
            try
            {
                return [.. _context.Pessoas.Include(p => p.Telefones)];
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
                return _context
                    .Pessoas.Include(p => p.Telefones)
                    .FirstOrDefault(p => p.Id_pessoa == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a pessoa por ID.", ex);
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

                _context.Update(pessoa);
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

            return saved > 0;
        }

        List<Pessoa> IUserRepository.GetPessoaByName(string nome)
        {
            try
            {
                return [.. _context.Pessoas.Where(p => p.Nome == nome)];
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter a pessoa por nome.", ex);
            }
        }

        public Pessoa GetPessoaByTelefone(string id)
        {
            try
            {
                return _context
                    .Pessoas.Include(p => p.Telefones)
                    .FirstOrDefault(p => p.Telefones.Any(t => t.Numero == id));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter pessoas pelo telefone.", ex);
            }
        }
    }
}
