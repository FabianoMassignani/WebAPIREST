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
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            try
            {
                var telefones = _telefoneRepository.GetAllTelefone();

                var telefonesDto = _mapper.Map<IEnumerable<TelefoneDto>>(telefones);

                return Ok(telefonesDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TelefoneDto))]
        [ProducesResponseType(404)]
        public IActionResult GetById(int id)
        {
            try
            {
                var telefone = _telefoneRepository.GetTelefoneById(id);

                if (telefone == null)
                    return NotFound("Telefone não encontrado");

                var telefoneDto = _mapper.Map<TelefoneDto>(telefone);

                return Ok(telefoneDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }


        [HttpGet("GetByNumero")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PessoaDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetByNumero(string numero)
        {
            try
            {
                var pessoas = _telefoneRepository.GetPessoaByTelefone(numero);

                if (pessoas == null || !pessoas.Any())
                {
                    return NotFound("Nenhuma pessoa encontrada com o número de telefone especificado");
                }

                var pessoasDto = _mapper.Map<IEnumerable<PessoaDto>>(pessoas);

                return Ok(pessoasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }


        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TelefoneDto))]
        [ProducesResponseType(400)]
        public IActionResult Post([FromQuery] int id_pessoa, TelefoneViewModel telefoneView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_pessoaRepository.PessoaExist(id_pessoa))
                    return NotFound("Pessoa não encontrada");

                var pessoa = _pessoaRepository.GetPessoaById(id_pessoa);

                var telefone = new Telefone(telefoneView.Tipo, telefoneView.Numero);

                pessoa.Telefones.Add(telefone);
                _pessoaRepository.UpdatePessoa(pessoa);

                return Ok("Telefone associado com sucesso à pessoa");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(TelefoneDto))]
        [ProducesResponseType(400)]
        public IActionResult UpdateTelefone(int id, [FromBody] TelefoneViewModel telefoneViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (telefoneViewModel == null)
                    return BadRequest("Dados do Telefone inválidos");

                if (id != telefoneViewModel.Id_telefone)
                    return BadRequest("O ID informado na URL não corresponde ao ID do objeto");

                if (!_telefoneRepository.TelefoneExist(id))
                    return NotFound("Telefone não encontrado");

                var telefoneExistente = _telefoneRepository.GetTelefoneById(id);

                telefoneExistente.Numero = telefoneViewModel.Numero;
                telefoneExistente.Tipo = telefoneViewModel.Tipo;
                telefoneExistente.Id_telefone = id;
                telefoneExistente.PessoaId = telefoneExistente.PessoaId;

                _telefoneRepository.UpdateTelefone(telefoneExistente);

                return Ok("Telefone atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTelefone(int id)
        {
            try
            {
                if (!_telefoneRepository.TelefoneExist(id))
                {
                    return NotFound();
                }

                var telefoneToDelete = _telefoneRepository.GetTelefoneById(id);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_telefoneRepository.DeleteTelefone(telefoneToDelete))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o telefone");
                }

                return Ok("Telefone excluido com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex);
            }
        }
    }
}
