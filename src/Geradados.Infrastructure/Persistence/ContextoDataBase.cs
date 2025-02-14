using GeraDados.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Geradados.Infrastructure.Persistence;

public class ContextoDataBase : DbContext
{
    public ContextoDataBase()
    {

    }
    public ContextoDataBase(DbContextOptions<ContextoDataBase> options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(LocalDB)\\mssqllocaldb;database=GeraDadosDB");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Contato> Contatos { get; set; }
    public DbSet<TipoContato> TipoContatos { get; set; }
    public DbSet<TipoDeAtivo> TipoDeAtivos { get; set; }
    public DbSet<Ativo> Ativos { get; set; }
    public DbSet<Carteira> Carteiras { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pessoa>().Property(pessoa => pessoa.Nome).IsRequired();
        modelBuilder.Entity<Pessoa>().Property(pessoa => pessoa.Nome).HasColumnType("Varchar(max)");
        modelBuilder.Entity<Pessoa>().Property(pessoa => pessoa.DataNascimento).HasColumnType("Date");
        base.OnModelCreating(modelBuilder);
    }
}
