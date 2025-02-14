using Geradados.DataAccess.DB;
using GeraDados.DataModel.Models;
using GeraDados.DataModel.Repositories;

namespace Geradados.DataAccess.Repositories;

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