using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositorios;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntidadeBase
{
    DataHorsemanDbContext ctx;
    public RepositoryBase(DataHorsemanDbContext contexto)
    {
        ctx = contexto;
    }

    public void Excluir(T entity)
    {
        ctx.Set<T>().Remove(entity);
    }

    public T? ObterPorId(int id)
    {
        return ctx.Set<T>().FirstOrDefault(entity => entity.ID.Equals(id));
    }

    public IList<T> ObterTodos()
    {
        return ctx.Set<T>().ToList();
    }

    public void Salvar(T entity)
    {
        if (entity.ID.Equals(0))
            ctx.Set<T>().Add(entity);
    }
}