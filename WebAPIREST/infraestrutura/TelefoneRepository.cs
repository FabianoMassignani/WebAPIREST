﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPIREST.infraestrutura
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly ConnectionContext _context = new ConnectionContext();

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Telefone> GetAll()
        {
            try
            {
                return _context.Telefones.OrderBy(p => p.Id_telefone).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todos os telefones.", ex);
            }
        }

        public Telefone GetById(int id)
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

        public ICollection<Pessoa> GetPessoaByTelefone(string id)
        {
            try
            {
                return _context
                    .Pessoas.Include(p => p.Telefones)
                    .Where(p => p.Telefones.Any(t => t.Numero == id))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter pessoas pelo telefone.", ex);
            }
        }

        public void Post(Telefone telefone)
        {
            try
            {
                _context.Telefones.Add(telefone);
                _context.SaveChanges();
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

        public void Update(Telefone telefone)
        {
            try
            {
                if (_context.Entry(telefone).State == EntityState.Detached)
                {
                    _context.Attach(telefone);
                }

                _context.Entry(telefone).State = EntityState.Modified;

                _context.SaveChanges();
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
    }
}
