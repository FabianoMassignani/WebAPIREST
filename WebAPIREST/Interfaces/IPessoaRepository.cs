using System;
using System.Collections.Generic;
using WebAPIREST.Models;

namespace WebAPIREST.Interfaces
{
    public interface IPessoaRepository
    {
        List<Pessoa> GetAllPessoas();
        Pessoa GetPessoaById(int id);
        List<Pessoa> GetPessoaByName(string name);
        Pessoa GetPessoaByTelefone(string id);
        Pessoa GetPessoaByCPF(string cpf);
        bool PessoaExist(int id);
        bool CreatePessoa(Pessoa Pessoa);
        bool UpdatePessoa(Pessoa Pessoa);
        bool DeletePessoa(Pessoa Pessoa);
        bool Save();
    }
}
