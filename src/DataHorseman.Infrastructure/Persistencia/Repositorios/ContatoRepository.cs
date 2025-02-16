using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositorios;

public class ContatoRepository : RepositoryBase<Contato>, IContatoRepository
{
    DataHorsemanDbContext ctx = new DataHorsemanDbContext();
    public ContatoRepository(DataHorsemanDbContext contexto) : base(contexto)
    {
        ctx = contexto;
    }

    IList<Contato> IContatoRepository.ObtemContatosPorIdPessoa(int idPessoa)
    {
        return ctx.Contatos.Where(contato => contato.Pessoa.ID.Equals(idPessoa)).ToList();
    }
}