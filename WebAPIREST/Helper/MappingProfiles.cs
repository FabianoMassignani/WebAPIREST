using AutoMapper;
using WebAPIREST.Dto;
using WebAPIREST.Models;

namespace WebAPIREST.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UsersDto>();
            CreateMap<UsersDto, User>();
            CreateMap<Pessoa, PessoaDto>();
            CreateMap<Telefone, TelefoneDto>();
            CreateMap<TelefoneDto, Telefone>();
            CreateMap<EnderecoDto, Endereco>();
        }
    }
}
