using DataHorseman.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataHorseman.Infrastructure.Persistencia.Configurations;

public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.HasKey(p => p.ID);
        builder.Property(p => p.Nome).IsRequired().HasColumnType("varchar(max)");
        builder.Property(p => p.DataNascimento).HasColumnType("date");
        builder.HasIndex(p => p.CPF).HasDatabaseName("IX_Pessoa_CPF").IsUnique();
    }
}