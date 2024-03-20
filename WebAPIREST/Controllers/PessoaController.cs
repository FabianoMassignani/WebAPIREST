using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIREST.Dto;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using WebAPIREST.Repository;
using WebAPIREST.Utils;
using static WebAPIREST.Models.Pessoa;

namespace WebAPIREST.Controllers
{
    [Route("/pessoa")]
    [ApiController]
    public class PessoaController(
        IUserRepository pessoaRepository,
        IMapper mapper,
        Pessoa.PessoaValidator validator
    ) : ControllerBase
    {
        private readonly IUserRepository _pessoaRepository = pessoaRepository;
        private readonly IMapper _mapper = mapper;
        private readonly PessoaValidator _validator = validator;

        [HttpGet("all")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PessoaDto>))]
        [ProducesResponseType(500)]
        public IActionResult GetAllPessoas()
        {
            try
            {
                var pessoas = _mapper.Map<IEnumerable<PessoaDto>>(
                    _pessoaRepository.GetAllPessoas()
                );

                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet("GetById")]
        [ProducesResponseType(200, Type = typeof(PessoaDto))]
        [ProducesResponseType(500)]
        public IActionResult GetPessoaById([FromQuery] int Id_pessoa)
        {
            try
            {
                if (!_pessoaRepository.PessoaExist(Id_pessoa))
                    return NotFound("Pessoa não encontrada");

                var pessoa = _mapper.Map<PessoaDto>(_pessoaRepository.GetPessoaById(Id_pessoa));

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet("GetByNome")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PessoaDto>))]
        [ProducesResponseType(500)]
        public IActionResult GetPessoaByName([FromQuery] string nome)
        {
            try
            {
                var pessoas = _mapper.Map<IEnumerable<PessoaDto>>(
                    _pessoaRepository.GetPessoaByName(nome)
                );

                if (pessoas == null)
                    return NotFound("Nenhuma pessoa encontrada com o nome especificado");

                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet("GetPessoaByTelefone")]
        [ProducesResponseType(200, Type = typeof(PessoaDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetByNumero([FromQuery] string numero)
        {
            try
            {
                var pessoa = _pessoaRepository.GetPessoaByTelefone(numero);

                if (pessoa == null)
                    return NotFound(
                        "Nenhuma pessoa encontrada com o número de telefone especificado"
                    );

                var pessoaMap = _mapper.Map<PessoaDto>(pessoa);

                return Ok(pessoaMap);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(PessoaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreatePessoa([FromBody] PessoaNewUpdateDto pessoaCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!CpfCnpjUtils.IsValid(pessoaCreate.Cpf))
                    return BadRequest("CPF inválido");

                var endereco = new Endereco
                {
                    Rua = pessoaCreate.Rua,
                    Numero = pessoaCreate.Numero,
                    Complemento = pessoaCreate.Complemento,
                    Cidade = pessoaCreate.Cidade,
                    Estado = pessoaCreate.Estado,
                    CEP = pessoaCreate.CEP
                };

                var pessoa = new Pessoa(
                    pessoaCreate.Nome,
                    pessoaCreate.Data_nascimento,
                    pessoaCreate.Ativo,
                    pessoaCreate.Cpf,
                    pessoaCreate.Genero,
                    pessoaCreate.Email,
                    DateTime.Now,
                    DateTime.Now
                )
                {
                    Endereco = endereco
                };

                var validationResult = _validator.Validate(pessoa);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                if (!_pessoaRepository.CreatePessoa(pessoa))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao criar a pessoa");
                    return StatusCode(500, ModelState);
                }

                return CreatedAtAction(
                    nameof(GetPessoaById),
                    new { id = pessoa.Id_pessoa },
                    pessoa
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(200, Type = typeof(PessoaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdatePessoa(
            [FromQuery] int Id_pessoa,
            [FromBody] PessoaNewUpdateDto updatePessoa
        )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_pessoaRepository.PessoaExist(Id_pessoa))
                    return NotFound("Pessoa não encontrada");

                if (!CpfCnpjUtils.IsValid(updatePessoa.Cpf))
                    return BadRequest("CPF inválido");

                var endereco = new Endereco
                {
                    Rua = updatePessoa.Rua,
                    Numero = updatePessoa.Numero,
                    Complemento = updatePessoa.Complemento,
                    Cidade = updatePessoa.Cidade,
                    Estado = updatePessoa.Estado,
                    CEP = updatePessoa.CEP
                };

                Pessoa pessoa = new()
                {
                    Id_pessoa = Id_pessoa,
                    Nome = updatePessoa.Nome,
                    Data_nascimento = updatePessoa.Data_nascimento,
                    Ativo = updatePessoa.Ativo,
                    Cpf = updatePessoa.Cpf,
                    Genero = updatePessoa.Genero,
                    Email = updatePessoa.Email,
                    Data_atualizacao = DateTime.Now,
                    Data_cadastro = updatePessoa.Data_cadastro,
                    Endereco = endereco
                };

                var validationResult = _validator.Validate(pessoa);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                if (!_pessoaRepository.UpdatePessoa(pessoa))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao editar a pessoa");
                    return StatusCode(500, ModelState);
                }

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeletePessoa([FromQuery] int Id_pessoa)
        {
            try
            {
                if (!_pessoaRepository.PessoaExist(Id_pessoa))
                {
                    return NotFound("Pessoa não encontrada");
                }

                var pessoaToDelete = _pessoaRepository.GetPessoaById(Id_pessoa);

                if (!_pessoaRepository.DeletePessoa(pessoaToDelete))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a pessoa");
                    return StatusCode(500, ModelState);
                }

                return Ok("Pessoa excluída com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex);
            }
        }
    }
}
