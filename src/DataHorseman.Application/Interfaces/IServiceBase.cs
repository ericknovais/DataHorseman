using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Interfaces;

public interface IServiceBase<T> where T : EntidadeBase
{
    Task CriarNovoAsync(T entidade);
    Task Excluir(T entidade);
    Task Atualiza(T entidade);
    Task<T?> ObterPorIdAsync(int id);
    Task<IList<T>> ObterTodosAsync();
    Task<int> SaveChangesAsync();
}