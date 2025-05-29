using DataHorseman.Domain.Entidades;

namespace DataHorseman.Domain.Interfaces;

public interface IRepositoryBase<T> where T : EntidadeBase
{
    Task CriarNovoAsync(T entidade);
    Task CriarEmLoteAsync(IEnumerable<T> entidades);
    void Excluir(T entidade);
    void Atualiza(T entidade);
    Task<T?> ObterPorIdAsync(int id);
    Task<IList<T>> ObterTodosAsync();
    Task<int> SaveChangesAsync();
}