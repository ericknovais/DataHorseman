using GeraDados.DataModel.Models;

namespace GeraDados.DataModel.Repositories;

public interface IContatoRepository : IRepositoryBase<Contato>
{
    IList<Contato> ObtemContatosPorIdPessoa(int idPessoa);
}
