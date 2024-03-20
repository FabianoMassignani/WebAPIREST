using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIREST.Dto;
using WebAPIREST.infraestrutura;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using WebAPIREST.Utils;
using static WebAPIREST.Models.Telefone;

namespace WebAPIREST.Controllers
{
    [Route("/telefone")]
    [ApiController]
    public class TelefoneController(
        ITelefoneRepository telefoneRepository,
        IUserRepository pessoaRepository,
        Telefone.TelefoneValidator validator,
        IMapper mapper
    ) : ControllerBase
    {
        private readonly ITelefoneRepository _telefoneRepository = telefoneRepository;
        private readonly IUserRepository _pessoaRepository = pessoaRepository;
        private readonly TelefoneValidator _validator = validator;
        private readonly IMapper _mapper = mapper;

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TelefoneDto>))]
        [ProducesResponseType(500)]
        public IActionResult Get()
        {
            try
            {
                var telefones = _mapper.Map<IEnumerable<TelefoneDto>>(
                    _telefoneRepository.GetAllTelefone()
                );

                return Ok(telefones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TelefoneDto))]
        [ProducesResponseType(404)]
        public IActionResult GetById(string id)
        {
            try
            {
                if (!_telefoneRepository.TelefoneExist(Int32.Parse(id)))
                    return NotFound("Telefone não encontrado");

                var telefone = _mapper.Map<TelefoneDto>(
                    _telefoneRepository.GetTelefoneById(Int32.Parse(id))
                );

                return Ok(telefone);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(TelefoneDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromQuery] int id_pessoa, TelefoneNewDto createdTelefone)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_pessoaRepository.PessoaExist(id_pessoa))
                    return NotFound("Pessoa não encontrada para associar o telefone");

                if (!TelefoneUtils.IsTipoCorreto(createdTelefone.Tipo))
                    return BadRequest("Tipo de Telefone inválido");

                if (_telefoneRepository.GetByNumero(createdTelefone.Numero))
                    return NotFound("Telefone já cadastrado");

                var pessoa = _pessoaRepository.GetPessoaById(id_pessoa);

                var telefone = _mapper.Map<Telefone>(createdTelefone);

                var validationResult = _validator.Validate(telefone);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                pessoa.Telefones.Add(telefone);

                if (!_pessoaRepository.UpdatePessoa(pessoa))
                {
                    ModelState.AddModelError(
                        "",
                        "Ocorreu um erro ao  associar o telefone à pessoa"
                    );
                    return StatusCode(500, ModelState);
                }

                return Ok("Telefone associado à pessoa com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(TelefoneUpdateDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateTelefone([FromQuery] int Id_telefone, [FromBody] TelefoneUpdateDto updateTelefone)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_telefoneRepository.TelefoneExist(Id_telefone))
                    return NotFound("Telefone não encontrado");

                var telefone = _mapper.Map<Telefone>(updateTelefone);

                var validationResult = _validator.Validate(telefone);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                if (!_telefoneRepository.UpdateTelefone(telefone))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao editar o telefone");
                    return StatusCode(500, ModelState);
                }

                return Ok("Telefone atualizado com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex);
            }
        }

        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTelefone([FromQuery] int id)
        {
            try
            {
                if (!_telefoneRepository.TelefoneExist(id))
                {
                    return NotFound("Telefone não encontrado");
                }

                var telefoneToDelete = _telefoneRepository.GetTelefoneById(id);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_telefoneRepository.DeleteTelefone(telefoneToDelete))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o telefone");
                    return StatusCode(500, ModelState);
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
