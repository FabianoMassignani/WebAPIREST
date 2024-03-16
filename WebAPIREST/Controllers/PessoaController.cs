using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIREST.Dto;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using WebAPIREST.Repository;
using WebAPIREST.ViewModel;

namespace WebAPIREST.Controllers
{
    [Route("/pessoa")]
    [ApiController]
    public class PessoaController(IPessoaRepository pessoaRepository, IMapper mapper)
        : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PessoaDto>))]
        [ProducesResponseType(500)]
        public IActionResult GetAllPessoas()
        {
            try
            {
                var pessoas = _pessoaRepository.GetAllPessoas();

                var pessoasDto = _mapper.Map<IEnumerable<PessoaDto>>(pessoas);

                return Ok(pessoasDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(PessoaDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetPessoaById(int id)
        {
            try
            {
                if (!_pessoaRepository.PessoaExist(id))
                    return NotFound("Pessoa não encontrada");

                var pessoa = _mapper.Map<PessoaDto>(_pessoaRepository.GetPessoaById(id));

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet("/GetByNome/{nome}")]
        [ProducesResponseType(200, Type = typeof(PessoaDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetPessoaByName(string nome)
        {
            try
            {
                var pessoa = _pessoaRepository.GetPessoaByName(nome);

                if (pessoa == null)
                    return NotFound("Pessoa não encontrada com o nome especificado");

                return Ok(pessoa);
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
        public IActionResult CreatePessoa(PessoaViewModel pessoaView)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var pessoa = new Pessoa(
                    pessoaView.Nome,
                    pessoaView.Data_nascimento,
                    pessoaView.Ativo,
                    pessoaView.Cpf,
                    pessoaView.Genero,
                    pessoaView.Endereco,
                    pessoaView.Email,
                    DateTime.Now 
                );

                _pessoaRepository.CreatePessoa(pessoa);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(PessoaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdatePessoa(int id, [FromBody] PessoaViewModel pessoaViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (pessoaViewModel == null)
                    return BadRequest("Dados da pessoa inválidos");

                if (id != pessoaViewModel.Id_pessoa)
                    return BadRequest("O ID informado na URL não corresponde ao ID do objeto");

                if (!_pessoaRepository.PessoaExist(id))
                    return NotFound("Pessoa não encontrada");

                Pessoa pessoa = new Pessoa(
                    pessoaViewModel.Nome,
                    pessoaViewModel.Data_nascimento,
                    pessoaViewModel.Ativo,
                    pessoaViewModel.Cpf,
                    pessoaViewModel.Genero,
                    pessoaViewModel.Endereco,
                    pessoaViewModel.Email,
                    DateTime.Now
                );

                pessoa.Id_pessoa = id;

                _pessoaRepository.UpdatePessoa(pessoa);

                var pessoaDto = _mapper.Map<PessoaDto>(pessoa);

                return Ok(pessoaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePessoa(int id)
        {
            try
            {
                if (!_pessoaRepository.PessoaExist(id))
                {
                    return NotFound();
                }

                var pessoaToDelete = _pessoaRepository.GetPessoaById(id);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_pessoaRepository.DeletePessoa(pessoaToDelete))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o pessoa");
                }

                return Ok("Pessoa excluido com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex);
            }
        }
    }



    }
