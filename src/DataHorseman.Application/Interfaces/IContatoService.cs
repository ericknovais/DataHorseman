using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Interfaces;

public interface IContatoService : IServiceBase<Contato>
{
    IList<Contato> ObtemContatosPorIdPessoa(int idPessoa);
}