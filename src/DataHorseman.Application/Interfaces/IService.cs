namespace DataHorseman.Application.Interfaces;

public interface IService
{
    void SaveChanges();
    public IAtivoService AtivoService { get; }
    public ICarteiraService CarteiraService { get; }
    public IContatoService ContatoService { get; }
    public IEnderecoService EnderecoService { get; }
    public IPessoaService PessoaService { get; }
    public ITipoContatoService TipoContatoService { get; }
    public ITipoDeAtivoService TipoDeAtivoService { get; }
}
