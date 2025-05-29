using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataHorseman.Infrastructure.Persistencia.Repositories;

public class Repository : IRepository
{
    DataHorsemanDbContext ctx;
    private IDbContextTransaction? _transaction;
    public Repository()
    {
        ctx = new DataHorsemanDbContext();
    }

    IPessoaRepository? pessoa;
    public IPessoaRepository Pessoa { get { return pessoa ?? (pessoa = new PessoaRepository(ctx)); } }

    IEnderecoRepository? endereco;
    public IEnderecoRepository Endereco { get { return endereco ?? (endereco = new EnderecoRepository(ctx)); } }

    IContatoRepository? contato;
    public IContatoRepository Contato { get { return contato ?? (contato = new ContatoRepository(ctx)); } }

    ITipoContatoRepository? tipoContato;
    public ITipoContatoRepository TipoContato { get { return tipoContato ?? (tipoContato = new TipoContatoRepository(ctx)); } }

    ITipoDeAtivoRepository? tipoDeAtivo;
    public ITipoDeAtivoRepository TipoDeAtivo { get { return tipoDeAtivo ?? (tipoDeAtivo = new TipoDeAtivoRepository(ctx)); } }

    IAtivoRepository? ativo;
    public IAtivoRepository Ativo { get { return ativo ?? (ativo = new AtivoRepository(ctx)); } }

    ICarteiraRepository? carteira;
    public ICarteiraRepository Carteira { get { return carteira ?? (carteira = new CarteiraRepository(ctx)); } }

    public void SaveChanges()
    {
        ctx.SaveChanges();
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
            _transaction = await ctx.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("Nenhuma transação foi iniciada.");

        try
        {
            await _transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            // Aqui você pode registrar ou fazer log da exceção se necessário
            throw new InvalidOperationException("Erro ao realizar commit na transação.", ex);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("Nenhuma transação foi iniciada para fazer rollback.");

        try
        {
            await _transaction.RollbackAsync();
        }
        catch (Exception ex)
        {
            // Aqui você pode registrar ou fazer log da exceção se necessário
            throw new InvalidOperationException("Erro ao realizar rollback na transação.", ex);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}