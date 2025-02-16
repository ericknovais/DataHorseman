using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositorios;

public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
{
    DataHorsemanDbContext ctx = new DataHorsemanDbContext();
    public PessoaRepository(DataHorsemanDbContext contexto) : base(contexto)
    {
        ctx = contexto;
    }

    public Pessoa? ObtemPessoaPorCPF(string cpf)
    {
        return ctx.Pessoas.FirstOrDefault(pessoa => pessoa.CPF.Equals(cpf));
    }
}