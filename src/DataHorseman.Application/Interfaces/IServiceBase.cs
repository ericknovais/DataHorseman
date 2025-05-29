using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Interfaces;

public interface IServiceBase<T> where T : class
{
    Task CriarNovoAsync(T entidade);
    Task CriarEmLoteAsync(IEnumerable<T> entidades);
    Task ExcluirAsync(T entidade);
    Task AtualizaAsync(T entidade);
    Task<T?> ObterPorIdAsync(int id);
    Task<IList<T>> ObterTodosAsync();
    Task<int> SaveChangesAsync();
}