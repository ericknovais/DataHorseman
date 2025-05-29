using DataHorseman.Infrastructure.Persistencia.Dtos;

namespace DataHorseman.Application.UseCases.CadastrarPessoa;

public interface ICadastroPessoaUseCase
{
    Task ProcessaPessoaAsync(PessoaJson pessoaJson);
}
