using DataHorseman.Domain.Entities;

namespace DataHorseman.Domain.Repositories;

public interface IRepositoryBase<T> where T : EntityBase
{
    void Salvar(T entity);
    void Excluir(T entity);
    T? ObterPorId(int id);
    IList<T> ObterTodos();
}
