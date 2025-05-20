using DataHorseman.Domain.Interfaces;
using DataHorseman.Infrastructure.Persistencia.DataContext;

namespace DataHorseman.Infrastructure.Persistencia.Repositories;

public class Repository : IRepository
{
    DataHorsemanDbContext ctx;
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
}