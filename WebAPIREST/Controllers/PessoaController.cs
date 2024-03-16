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
    public class PessoaController(IPessoaRepository pessoaRepository, IMapper mapper) : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Telefone))]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {

            var pessoas = _pessoaRepository.GetAll();

            var pessoasDto = _mapper.Map<IEnumerable<PessoaDto>>(pessoas);

            return Ok(pessoasDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Pessoa))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            if (!_pessoaRepository.PessoaExist(id))
                return NotFound();

            var pessoa = _mapper.Map<PessoaDto>(_pessoaRepository.GetById(id));
          
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pessoa);
        }

        [HttpGet("/GetByNome/{nome}")]
        [ProducesResponseType(200, Type = typeof(Pessoa))]
        [ProducesResponseType(400)]
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
        [ProducesResponseType(200, Type = typeof(Telefone))]
        [ProducesResponseType(400)]
        public IActionResult Post(PessoaViewModel pessoaView)
        {
            var pessoa = new Pessoa(
                pessoaView.Nome,
                pessoaView.Data_nascimento,
                pessoaView.Ativo,
                pessoaView.Cpf,
                pessoaView.Genero,
                pessoaView.Endereco,
                pessoaView.Email,
                pessoaView.Data_atualizacao
            );

            _pessoaRepository.Post(pessoa);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(PessoaViewModel pessoa)
        {

            return Ok(pessoa);

        }
    }
}
