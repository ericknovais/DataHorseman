using Geradados.DataAccess.DB;
using GeraDados.DataModel.Models;
using GeraDados.DataModel.Repositories;

namespace Geradados.DataAccess.Repositories;

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