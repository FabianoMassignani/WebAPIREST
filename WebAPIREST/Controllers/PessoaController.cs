using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIREST.Dto;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using WebAPIREST.Repository;
using WebAPIREST.Utils;
using WebAPIREST.ViewModel;

namespace WebAPIREST.Controllers
{
    [Route("/pessoa")]
    [ApiController]
    public class PessoaController(IPessoaRepository pessoaRepository, ITelefoneRepository telefoneRepository, IMapper mapper)
        : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository = pessoaRepository;
        private readonly ITelefoneRepository _telefoneRepository = telefoneRepository;
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
        public IActionResult GetPessoaById(int id)
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
        [ProducesResponseType(200, Type = typeof(PessoaDto))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetPessoaByName(string nome)
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

        [HttpGet("/GetPessoaByNumero")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PessoaDto>))]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetByNumero(string numero)
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
        public IActionResult CreatePessoa(PessoaViewModel pessoaView)
        {
            try
            {
                if (pessoaView == null)
                    return BadRequest(ModelState);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!CpfCnpjUtils.IsValid(pessoaView.Cpf))
                    return BadRequest("CPF inválido");

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

                if (!CpfCnpjUtils.IsValid(pessoaViewModel.Cpf))
                    return BadRequest("CPF inválido");

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
