using Geradados.Infrastructure.Persistence;
using GeraDados.Domain.Entities;
using GeraDados.Domain.Repositories;

namespace Geradados.Infrastructure.Intercafes;

public class ContatoRepository : RepositoryBase<Contato>, IContatoRepository
{
    ContextoDataBase ctx = new ContextoDataBase();
    public ContatoRepository(ContextoDataBase contexto) : base(contexto)
    {
        ctx = contexto;
    }

    IList<Contato> IContatoRepository.ObtemContatosPorIdPessoa(int idPessoa)
    {
        return ctx.Contatos.Where(contato => contato.Pessoa.ID.Equals(idPessoa)).ToList();
    }
}