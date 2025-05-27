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

        AtivoService = new AtivoService(_repository.Ativo, _mapper);
        CarteiraService = new CarteiraService(_repository.Carteira, _mapper);
        PessoaService = new PessoaService(_repository.Pessoa, _mapper);
        ArquivoService = new ArquivoService();
        EnderecoService = new EnderecoService(_repository.Endereco, _mapper, _repository.Pessoa);
    }

    public IAtivoService AtivoService { get; }

    public IContatoService ContatoService => throw new NotImplementedException();

    public IEnderecoService EnderecoService { get; }

    public IPessoaService PessoaService { get; }

    public ITipoContatoService TipoContatoService => throw new NotImplementedException();

    public ITipoDeAtivoService TipoDeAtivoService => throw new NotImplementedException();

    public ICarteiraService CarteiraService { get; }

    public IArquivoService ArquivoService { get; }

    public async Task BeginTransactionAsync() => await _repository.BeginTransactionAsync();
    public async Task CommitTransactionAsync() => await _repository.CommitTransactionAsync();
    public async Task RollbackTransactionAsync() => await _repository.RollbackTransactionAsync();
    public void SaveChanges() => _repository.SaveChanges();
}