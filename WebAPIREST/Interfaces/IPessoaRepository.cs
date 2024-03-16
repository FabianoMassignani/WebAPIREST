﻿using WebAPIREST.Models;

namespace WebAPIREST.Interfaces
{
    public interface IPessoaRepository
    {
        List<Pessoa> GetAll();
        Pessoa GetById(int id);
        Pessoa GetByNome(string nome);
        Pessoa GetByCPF(string cpf);
        bool PessoaExist(int id);
        void Post(Pessoa pessoa);
        void Update(Pessoa entity);
        void Delete(int id);
    }
}