using AutoMapper;
using WebAPIREST.Dto;
using WebAPIREST.Models;

namespace WebAPIREST.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pessoa, PessoaDto>();
            CreateMap<Telefone, TelefoneDto>();
        }
    }
}
