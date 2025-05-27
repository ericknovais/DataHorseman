using DataHorseman.Domain.Entidades;
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
        modelBuilder.Entity<Pessoa>().Property(pessoa => pessoa.Nome).IsRequired();
        modelBuilder.Entity<Pessoa>().Property(pessoa => pessoa.Nome).HasColumnType("Varchar(max)");
        modelBuilder.Entity<Pessoa>().Property(pessoa => pessoa.DataNascimento).HasColumnType("Date");
        modelBuilder.Entity<Pessoa>().HasIndex(pessoa => pessoa.CPF).HasDatabaseName("IX_Pessoa_CPF").IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}
