using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Profiles;

public class AtivoProfile : Profile
{
    public AtivoProfile()
    {
        CreateMap<Ativo,AtivoDto>();
    }
}