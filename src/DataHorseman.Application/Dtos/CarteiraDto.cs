﻿using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Dtos;

public class CarteiraDto
{
    public Pessoa Pessoa { get; set; } = new Pessoa();
    public List<AtivoDto> Acoes { get; set; } = new List<AtivoDto>();
    public List<AtivoDto> FundosImobiliarios { get; set; } = new List<AtivoDto>();

    public static CarteiraDto NovaCarteiraDto(Pessoa pessoa, List<AtivoDto> acoes, List<AtivoDto> fiis)
    {
        return new CarteiraDto()
        {
            Pessoa = pessoa,
            Acoes = acoes,
            FundosImobiliarios = fiis
        };
    }
}
