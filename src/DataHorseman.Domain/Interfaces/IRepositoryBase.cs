using DataHorseman.Domain.Entidades;

namespace DataHorseman.Domain.Interfaces;

public interface IRepositoryBase<T> where T : EntidadeBase
{
    void Salvar(T entity);
    void Excluir(T entity);
    T? ObterPorId(int id);
    IList<T> ObterTodos();
}
