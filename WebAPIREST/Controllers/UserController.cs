using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPIREST.Dto;
using WebAPIREST.Interfaces;
using WebAPIREST.Models;
using WebAPIREST.Repository;
using WebAPIREST.Services;
using static WebAPIREST.Models.Pessoa;
using static WebAPIREST.Models.User;

namespace WebAPIREST.Controllers
{
    [Route("/usuario")]
    [ApiController]
    public class UserController(IUsersRepository usersRepository, User.UserValidator validator, IMapper mapper) : ControllerBase
    {
        private readonly IUsersRepository _usersRepository = usersRepository;
        private readonly TokenService _tokenService = new(Settings.Secret);
        private readonly UserValidator _validator = validator;
        private readonly IMapper _mapper = mapper;


        [HttpGet("all")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UsersDto>))]
        [ProducesResponseType(500)]
        public IActionResult GetAllUsers()
        {
            try
            {
                var userDtos = _mapper.Map<IEnumerable<UsersDto>>(_usersRepository.GetAllUsers());

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(201, Type = typeof(UsersDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult RegisterUser([FromBody] UsersDto userCreate)
        {
            try
            {
                var existingUser = _usersRepository.GetByUsername(userCreate.Username);

                if (existingUser != null)
                {
                    return BadRequest("Nome de usuário já existe");
                }

                var user = _mapper.Map<User>(userCreate);

                var validationResult = _validator.Validate(user);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                _usersRepository.CreateUser(user);

                var userDto = _mapper.Map<UsersDto>(user);

                return CreatedAtAction(nameof(RegisterUser), userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(200, Type = typeof(LoginResponseDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Login([FromBody] UsersLoginDto loginDto)
        {
            try
            {
                var user = _usersRepository.GetByUsernameAndPassword(
                    loginDto.Username,
                    loginDto.Password
                );

                if (user == null)
                {
                    return Unauthorized("Nome de usuário ou senha inválidos");
                }

                var token = _tokenService.GenerateToken(user);

                var responseDto = new LoginResponseDto { Token = token };

                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteTelefone([FromBody] UsersLoginDto loginDto)
        {
            try
            {
                var user = _usersRepository.GetByUsernameAndPassword(
                    loginDto.Username,
                    loginDto.Password
                );

                if (user == null)
                {
                    return Unauthorized("Nome de usuário ou senha inválidos");
                }

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!_usersRepository.DeleteUser(user))
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o usuário");
                    return StatusCode(500, ModelState);
                }

                return Ok("Usuário excluido com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex);
            }
        }
    }
}
