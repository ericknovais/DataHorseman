using Geradados.Infrastructure.Persistence;
using GeraDados.Domain.Entities;
using GeraDados.Domain.Repositories;

namespace Geradados.Infrastructure.Intercafes;

public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
{
    ContextoDataBase ctx = new ContextoDataBase();
    public PessoaRepository(ContextoDataBase contexto) : base(contexto)
    {
        ctx = contexto;
    }

    public Pessoa? ObtemPessoaPorCPF(string cpf)
    {
        return ctx.Pessoas.FirstOrDefault(pessoa => pessoa.CPF.Equals(cpf));
    }
}