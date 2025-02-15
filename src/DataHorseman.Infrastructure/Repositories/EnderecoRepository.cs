using DataHorseman.Domain.Entities;
using DataHorseman.Domain.Repositories;
using DataHorseman.Infrastructure.Persistence;

namespace DataHorseman.Infrastructure.Intercafes;

public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository
{
    ContextoDataBase ctx = new ContextoDataBase();
    public EnderecoRepository(ContextoDataBase contexto) : base(contexto)
    {
        ctx = contexto;
    }

    public Endereco? ObtemEnderecoPorIdPessoa(int idPessoa)
    {
        return ctx.Enderecos.FirstOrDefault(endereco => endereco.Pessoa.ID.Equals(idPessoa));
    }
}
