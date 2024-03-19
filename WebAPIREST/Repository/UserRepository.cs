using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebAPIREST.infraestrutura;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;

namespace WebAPIREST.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ConnectionContext _context = new();

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return [.. _context.Users];
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todos os usuários.", ex);
            }
        }

        public bool CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                return Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao criar usuário.", ex);
            }
        }

        public User GetById(int id)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Id_user == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter usuário pelo ID.", ex);
            }
        }

        public User GetByUsername(string username)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Username == username);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter usuário pelo nome de usuário.", ex);
            }
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            try
            {
                return _context.Users.FirstOrDefault(u =>
                    u.Username == username && u.Password == password
                );
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter usuário pelo nome de usuário e senha.", ex);
            }
        }

        public bool DeleteUser(User user)
        {
            try
            {
                _context.Remove(user);
                return Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar usuario.", ex);
            }
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();

            return saved > 0;
        }
    }
}
