using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositories;

public class EnderecoRepository : RepositoryBase<Endereco>, IEnderecoRepository
{
    DataHorsemanDbContext ctx = new DataHorsemanDbContext();
    public EnderecoRepository(DataHorsemanDbContext contexto) : base(contexto)
    {
        ctx = contexto;
    }

    public Endereco? ObtemEnderecoPorIdPessoa(int idPessoa)
    {
        return ctx.Enderecos.FirstOrDefault(endereco => endereco.Pessoa.ID.Equals(idPessoa));
    }
}
