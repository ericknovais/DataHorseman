using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Profiles;

public class AtivoProfile : Profile
{
    public AtivoProfile()
    {
        CreateMap<Ativo,AtivoDto>();
        CreateMap<AtivoDto,Ativo>().ForMember(dest => dest.ID, opt => opt.Ignore()); ;
    }
}