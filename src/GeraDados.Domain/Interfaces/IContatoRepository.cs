using GeraDados.Domain.Entities;

namespace GeraDados.Domain.Repositories;

public interface IContatoRepository : IRepositoryBase<Contato>
{
    IList<Contato> ObtemContatosPorIdPessoa(int idPessoa);
}
