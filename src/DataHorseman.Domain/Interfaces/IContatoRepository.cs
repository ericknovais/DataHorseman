using DataHorseman.Domain.Entities;

namespace DataHorseman.Domain.Repositories;

public interface IContatoRepository : IRepositoryBase<Contato>
{
    IList<Contato> ObtemContatosPorIdPessoa(int idPessoa);
}
