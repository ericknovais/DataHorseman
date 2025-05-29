using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Profiles
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<Pessoa, PessoaDto>();
            CreateMap<PessoaDto, Pessoa>()
                .ForMember(dest => dest.ID, opt => opt.Ignore());
        }
    }
}