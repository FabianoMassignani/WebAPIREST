using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIREST.Dto;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using WebAPIREST.Repository;
using WebAPIREST.Utils;

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
                var pessoas = _mapper.Map<IEnumerable<PessoaDto>>(
                    _pessoaRepository.GetAllPessoas()
                );

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(pessoas);
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
        public IActionResult GetPessoaById([FromQuery] int id)
        {
            try
            {
                if (!_pessoaRepository.PessoaExist(id))
                    return NotFound("Pessoa não encontrada");

                var pessoa = _mapper.Map<PessoaDto>(_pessoaRepository.GetPessoaById(id));

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(pessoa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet("/GetByNome")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PessoaDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetPessoaByName([FromQuery] string nome)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

  
                var pessoas = _mapper.Map<IEnumerable<PessoaDto>>(
                   _pessoaRepository.GetPessoaByName(nome)
               );

                return Ok(pessoas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpGet("/GetPessoaByTelefone")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PessoaDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetByNumero([FromQuery] string numero)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

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
        public IActionResult CreatePessoa([FromBody] PessoaDto pessoaCreate)
        {
            try
            {
                if (pessoaCreate == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!CpfCnpjUtils.IsValid(pessoaCreate.Cpf))
                    return BadRequest("CPF inválido");

                var pessoa = new Pessoa(
                    pessoaCreate.Nome,
                    pessoaCreate.Data_nascimento,
                    pessoaCreate.Ativo,
                    pessoaCreate.Cpf,
                    pessoaCreate.Genero,
                    pessoaCreate.Endereco,
                    pessoaCreate.Email,
                    DateTime.Now
                );

                if (!_pessoaRepository.CreatePessoa(pessoa))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao criar a pessoa");
                    return StatusCode(500, ModelState);
                }

                return Ok(new { Message = "Pessoa criada com sucesso.", Pessoa = pessoa });
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
        public IActionResult UpdatePessoa(int id, [FromBody] PessoaDto updatePessoa)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (updatePessoa == null)
                    return BadRequest("Dados da pessoa inválidos");

                if (id != updatePessoa.Id_pessoa)
                    return BadRequest("O ID informado na URL não corresponde ao ID do objeto");

                if (!_pessoaRepository.PessoaExist(id))
                    return NotFound("Pessoa não encontrada");

                if (!CpfCnpjUtils.IsValid(updatePessoa.Cpf))
                    return BadRequest("CPF inválido");

                Pessoa pessoa = new(
                    updatePessoa.Nome,
                    updatePessoa.Data_nascimento,
                    updatePessoa.Ativo,
                    updatePessoa.Cpf,
                    updatePessoa.Genero,
                    updatePessoa.Endereco,
                    updatePessoa.Email,
                    DateTime.Now
                )
                {
                    Id_pessoa = id
                };

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

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir a pessoa");
                    return StatusCode(500, ModelState);
                }

                return Ok("Pessoa excluida com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex);
            }
        }
    }
}
