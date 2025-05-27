using DataHorseman.Application.Dtos;
using DataHorseman.Domain.Entities;

namespace DataHorseman.Application.Extensions;

public static class EnderecoDtoExtensions
{
    public static EnderecoDados ToEnderecoDados(this EnderecoDto enderecoDto)
    {
        return new EnderecoDados
        {
            Logradouro = enderecoDto.Logradouro,
            CEP = enderecoDto.CEP,
            Bairro = enderecoDto.Bairro,
            Cidade = enderecoDto.Cidade,
            Estado = enderecoDto.Estado,
            Numero = Convert.ToInt32(enderecoDto.Numero)
        };
    }
}
