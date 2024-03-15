using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIREST.Dto;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using WebAPIREST.ViewModel;

namespace WebAPIREST.Controllers
{

    [Route("/pessoa")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;

        public PessoaController(IPessoaRepository pessoaRepository, IMapper mapper)
        {
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

        [HttpGet]
       // [ProducesResponseType(200, Type = typeof(Telefone))]
       // [ProducesResponseType(400)]
        public IActionResult Get()
        {
            var pessoas = _pessoaRepository.GetAll();

            return Ok(pessoas);
        }

        [HttpGet("{id}")]
       // [ProducesResponseType(200, Type = typeof(Pessoa))]
       // [ProducesResponseType(400)]
        public IActionResult GetById(int id_pessoa)
        {
            if (!_pessoaRepository.PessoaExist(id_pessoa))
                return NotFound();

            // var pessoa = _mapper.Map<PessoaDto>(_pessoaRepository.GetById(id_pessoa));
            var pessoa = _pessoaRepository.GetById(id_pessoa);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pessoa);
        }

        [HttpGet("/GetByNome/{nome}")]
       // [ProducesResponseType(200, Type = typeof(Pessoa))]
       // [ProducesResponseType(400)]
        public IActionResult GetByNome(string nome)
        {
            
            var pessoa = _pessoaRepository.GetByNome(nome);

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(pessoa);
        }

        [HttpPost]
       // [ProducesResponseType(200, Type = typeof(Telefone))]
      //  [ProducesResponseType(400)]
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
    }
}
