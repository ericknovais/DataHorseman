using DataHorseman.Domain.Entidades;
using DataHorseman.Infrastructure.Persistencia.Dtos;

namespace DataHorseman.Application.Mappers;

public static class PessoaJsonMapper
{
    public static List<(PessoaJson PessoaJson, Pessoa Pessoa)> MapearParaEntidadesValidas(this IList<PessoaJson> pessoasJson)
    {
        return pessoasJson
            .Where(p => DateTime.TryParse(p.Data_nasc, out _)) // filtra só os válidos
            .Select(p =>
            {
                DateTime.TryParse(p.Data_nasc, out DateTime dataNascimento);
                var pessoa = Pessoa.Novo(
                 nome: p.Nome,
                 cpf: p.CPF,
                 rg: p.RG,
                 sexo: p.Sexo,
                 dataNascimento: dataNascimento);
                return (PessoaJson: p, Pessoa: pessoa); // mapeia para PessoaJson e Pessoa
            }
        ).ToList();
    }
}