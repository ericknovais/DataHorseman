using DataHorseman.Domain.Entidades;
using DataHorseman.Infrastructure.Persistencia.Configurations;
using Microsoft.EntityFrameworkCore;

namespace DataHorseman.Infrastructure.Persistencia.DataContext;

public class DataHorsemanDbContext : DbContext
{
    public DataHorsemanDbContext()
    {

    }
    public DataHorsemanDbContext(DbContextOptions<DataHorsemanDbContext> options) : base(options)
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
        modelBuilder.ApplyConfiguration(new PessoaConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
