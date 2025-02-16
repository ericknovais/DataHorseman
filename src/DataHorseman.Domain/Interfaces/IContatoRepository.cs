using DataHorseman.Domain.Entidades;

namespace DataHorseman.Domain.Interfaces;

public interface IContatoRepository : IRepositoryBase<Contato>
{
    IList<Contato> ObtemContatosPorIdPessoa(int idPessoa);
}
