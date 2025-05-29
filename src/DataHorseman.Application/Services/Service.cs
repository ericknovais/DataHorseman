using AutoMapper;
using DataHorseman.Application.Interfaces;
using DataHorseman.Application.Profiles;
using DataHorseman.Domain.Interfaces;

namespace DataHorseman.Application.Services;

public class Service : IService
{
    protected readonly IRepository _repository;
    protected readonly IMapper _mapper;
    public Service(IRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
        
        ArquivoService = new ArquivoService();
        AtivoService = new AtivoService(_repository.Ativo, _mapper);
        CarteiraService = new CarteiraService(_repository.Carteira, _mapper);
        PessoaService = new PessoaService(_repository.Pessoa, _mapper);
        EnderecoService = new EnderecoService(_repository.Endereco, _mapper, _repository.Pessoa);
        ContatoService = new ContatosService(_repository.Contato, _mapper);
        TipoContatoService = new TipoContatoService(_repository.TipoContato, _mapper);
        TipoDeAtivoService = new TipoDeAtivoService(_repository.TipoDeAtivo, _mapper);
    }

    public IAtivoService AtivoService { get; }

    public IContatoService ContatoService { get; }

    public IEnderecoService EnderecoService { get; }

    public IPessoaService PessoaService { get; }

    public ITipoContatoService TipoContatoService { get; }

    public ITipoDeAtivoService TipoDeAtivoService { get; }

    public ICarteiraService CarteiraService { get; }

    public IArquivoService ArquivoService { get; }

    public async Task BeginTransactionAsync() => await _repository.BeginTransactionAsync();
    public async Task CommitTransactionAsync() => await _repository.CommitTransactionAsync();
    public async Task RollbackTransactionAsync() => await _repository.RollbackTransactionAsync();
    public void SaveChanges() => _repository.SaveChanges();
}