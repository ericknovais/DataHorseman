using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Profiles;

public class EnderecoProfile: Profile
{
    public EnderecoProfile()
    {
        CreateMap<Endereco,EnderecoDto>();
        CreateMap<EnderecoDto, Endereco>()
            .ForMember(dest => dest.ID, opt => opt.Ignore());
            //.ForMember(dest => dest.Pessoa, opt => opt.Ignore())
            //.ForMember(dest => dest.Pessoa.ID, opt => opt.Ignore());
    }
}