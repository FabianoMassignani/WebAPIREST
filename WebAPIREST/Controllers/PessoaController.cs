using Microsoft.AspNetCore.Mvc;
using WebAPIREST.Model;
using WebAPIREST.Models;
using WebAPIREST.ViewModel;

namespace WebAPIREST.Controllers
{
    [ApiController]
    [Route("/pessoa")]

    public class PessoaController : ControllerBase
    {

        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [HttpPost]
        public IActionResult Add(PessoaViewModel pessoaView)
        {

            var pessoa = new Pessoa(
                pessoaView.nome,
                pessoaView.data_nascimento,
                pessoaView.ativo,
                pessoaView.cpf, 
                pessoaView.genero,
                pessoaView.endereco,
                pessoaView.email,
                pessoaView.data_atualizacao
                );

           _pessoaRepository.Add(pessoa);

            return Ok();
        }


        [HttpGet]

        public IActionResult Get()
        {

            var pessoas = _pessoaRepository.Get();

            return Ok(pessoas);

        }
    }
}
