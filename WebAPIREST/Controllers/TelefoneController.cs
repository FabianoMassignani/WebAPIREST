using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIREST.Dto;
using WebAPIREST.infraestrutura;
using WebAPIREST.Models;
using WebAPIREST.ViewModel;
using System.Collections.Generic;
using System.Linq;
using WebAPIREST.Interfaces;

namespace WebAPIREST.Controllers
{
    [Route("/telefone")]
    [ApiController]
    public class TelefoneController : ControllerBase
    {
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly IMapper _mapper;

        public TelefoneController(ITelefoneRepository telefoneRepository,
            IPessoaRepository pessoaRepository,
            IMapper mapper)
        {
            _telefoneRepository = telefoneRepository;
            _pessoaRepository = pessoaRepository;
            _mapper = mapper;
        }

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
        //[ProducesResponseType(200, Type = typeof(PessoaDto))]
        //[ProducesResponseType(400)]
        public IActionResult GetById(int id)
        {
            var pessoa = _mapper.Map<PessoaDto>(_telefoneRepository.GetById(id));

            if (pessoa == null)
            {
                return NotFound();
            }

            return Ok(pessoa);
        }

        [HttpGet("GetByNumero")]
       // [ProducesResponseType(200, Type = typeof(IEnumerable<PessoaDto>))]
        //[ProducesResponseType(400)]
        public IActionResult GetByNumero(string numero)
        {
            // Aqui você precisa implementar a lógica para buscar pessoas pelo número de telefone
            var pessoas = _telefoneRepository.GetPessoaByTelefone(numero);

            if (pessoas == null || !pessoas.Any())
            {
                return NotFound();
            }

            var pessoasDto = _mapper.Map<IEnumerable<PessoaDto>>(pessoas);

            return Ok(pessoasDto);
        }

        [HttpPost]
        public IActionResult Add([FromQuery]int id_pessoa,TelefoneViewModel telefoneView)
        {
            var telefone = new Telefone(
                telefoneView.tipo,
                telefoneView.numero
            );

            telefone.Pessoa = _pessoaRepository.GetById(id_pessoa);

            _telefoneRepository.Add(telefone);

            return Ok("Successfully created");
        }
    }
}
