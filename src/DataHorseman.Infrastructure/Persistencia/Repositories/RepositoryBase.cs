using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DataHorseman.Infrastructure.Persistencia.Repositories;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : EntidadeBase
{
    private readonly DataHorsemanDbContext _contexto;
    private readonly DbSet<T> _dbSet;

    public RepositoryBase(DataHorsemanDbContext contexto)
    {
        _contexto = contexto;
        _dbSet = _contexto.Set<T>();
    }

    public void Atualiza(T entidade) => _dbSet.Update(entidade);

    public async Task CriarEmLoteAsync(IEnumerable<T> entidades) => await _dbSet.AddRangeAsync(entidades); 

    public async Task CriarNovoAsync(T entidade) => await _dbSet.AddAsync(entidade);

    public void Excluir(T entidade) => _dbSet.Remove(entidade);

    public async Task<T?> ObterPorIdAsync(int id) => await _dbSet.FirstOrDefaultAsync(entidade => entidade.ID.Equals(id));

    public async Task<IList<T>> ObterTodosAsync() => await _dbSet.ToListAsync();

    public async Task<int> SaveChangesAsync() => await _contexto.SaveChangesAsync(); 
}