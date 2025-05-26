namespace DataHorseman.Application.Interfaces;

public interface IService
{
    void SaveChanges();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    public IAtivoService AtivoService { get; }
    public ICarteiraService CarteiraService { get; }
    public IContatoService ContatoService { get; }
    public IEnderecoService EnderecoService { get; }
    public IPessoaService PessoaService { get; }
    public ITipoContatoService TipoContatoService { get; }
    public ITipoDeAtivoService TipoDeAtivoService { get; }

    /// <summary>
    /// Serviço responsável pela leitura e manipulação de arquivos.
    /// </summary>
    public IArquivoService ArquivoService { get; }
}
