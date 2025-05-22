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
    }

    public IAtivoService AtivoService { get; }

    public IContatoService ContatoService => throw new NotImplementedException();

    public IEnderecoService EnderecoService => throw new NotImplementedException();

    public IPessoaService PessoaService { get; }

    public ITipoContatoService TipoContatoService => throw new NotImplementedException();

    public ITipoDeAtivoService TipoDeAtivoService => throw new NotImplementedException();

    public ICarteiraService CarteiraService { get; }

    public IArquivoService ArquivoService { get; }

    public void SaveChanges()
    {
        _repository.SaveChanges();
    }
}