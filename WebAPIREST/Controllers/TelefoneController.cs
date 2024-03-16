using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIREST.Dto;
using WebAPIREST.infraestrutura;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using WebAPIREST.ViewModel;

namespace WebAPIREST.Controllers
{
    [Route("/telefone")]
    [ApiController]
    public class TelefoneController(
        ITelefoneRepository telefoneRepository,
        IPessoaRepository pessoaRepository,
        IMapper mapper
        ) : ControllerBase
    {
        private readonly ITelefoneRepository _telefoneRepository = telefoneRepository;
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TelefoneDto>))]
        [ProducesResponseType(400)]
        public IActionResult Get()
        {
            var telefones = _telefoneRepository.GetAll();

            var telefonesDto = _mapper.Map<IEnumerable<TelefoneDto>>(telefones);

            return Ok(telefonesDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PessoaDto))]
        [ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            if (!_telefoneRepository.TelefoneExist(id))
                return NotFound();

            var pessoa = _mapper.Map<TelefoneDto>(_telefoneRepository.GetById(id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pessoa);
        }

        [HttpGet("GetByNumero")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PessoaDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetByNumero(string numero)
        {
            var pessoas = _telefoneRepository.GetPessoaByTelefone(numero);

            if (pessoas == null)
            {
                return NotFound();
            }

            var pessoasDto = _mapper.Map<IEnumerable<PessoaDto>>(pessoas);

            return Ok(pessoasDto);
        }

        [HttpPost]
        public IActionResult Post([FromQuery] int id_pessoa, TelefoneViewModel telefoneView)
        {
            var pessoa = _pessoaRepository.GetById(id_pessoa);

            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada");
            }

            var telefone = new Telefone(telefoneView.Tipo, telefoneView.Numero);

            pessoa.Telefones.Add(telefone);  
            _pessoaRepository.Update(pessoa);

            return Ok("Telefone associado com sucesso à pessoa");
        }
    }
}
