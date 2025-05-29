using AutoMapper;
using DataHorseman.Application.Interfaces;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Interfaces;

namespace DataHorseman.Application.Services
{
    public class ContatosService : IContatoService
    {
        protected readonly IContatoRepository _contatoRepository;
        protected readonly IMapper _mapper;

        public ContatosService(IContatoRepository contatoService, IMapper mapper)
        {
            _contatoRepository = contatoService;
            _mapper = mapper;
        }

        public Task AtualizaAsync(Contato entidade)
        {
            throw new NotImplementedException();
        }

        public async Task CriarEmLoteAsync(IEnumerable<Contato> entidades)
        {
           await _contatoRepository.CriarEmLoteAsync(entidades);
        }

        public Task CriarNovoAsync(Contato entidade)
        {
            throw new NotImplementedException();
        }

        public Task ExcluirAsync(Contato entidade)
        {
            throw new NotImplementedException();
        }

        public IList<Contato> ObtemContatosPorIdPessoa(int idPessoa)
        {
            throw new NotImplementedException();
        }

        public Task<Contato?> ObterPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Contato>> ObterTodosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
