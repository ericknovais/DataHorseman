using GeraDados.DataModel.Models;

namespace GeraDados.DataModel.Repositories;

public interface IAtivoRepository : IRepositoryBase<Ativo>
{
    List<Ativo> ObtemAtivosPorTipoDeAtivo(TipoDeAtivo? tipoDeAtivo);
}