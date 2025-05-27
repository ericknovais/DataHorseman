using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;
using Microsoft.EntityFrameworkCore;

namespace DataHorseman.Infrastructure.Persistencia.Repositories;

public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
{
    DataHorsemanDbContext ctx = new DataHorsemanDbContext();
    public PessoaRepository(DataHorsemanDbContext contexto) : base(contexto)
    {
        ctx = contexto;
    }

    public async Task<Pessoa?> ObtemPessoaPorCPFAsync(string cpf) 
        => await ctx.Pessoas.FirstOrDefaultAsync(pessoa => pessoa.CPF.Equals(cpf));

    public async Task<List<Pessoa>> VerificaSePessoasJaCadastradasAsync(List<string> cpfs)
    {
        return await ctx.Pessoas.AsNoTracking()
            .Where(pessoa => cpfs.Contains(pessoa.CPF))
            .ToListAsync();
    }
}