using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
using DataHorseman.Domain.Entidades;
using DataHorseman.Infrastructure.Persistencia.Dtos;

namespace DataHorseman.Application.UseCases.CadastrarPessoa
{
    public class CadastroPessoaUseCase : ICadastroPessoaUseCase
    {
        private readonly IService _service;
        private readonly IList<TipoContato> _listaTipoContatos;
        private readonly List<AtivoDto> _acoes;
        private readonly List<AtivoDto> _fiis;

        public CadastroPessoaUseCase(IService service, IList<TipoContato> listaTipoContatos, List<AtivoDto> acoes, List<AtivoDto> fiis)
        {
            _service = service;
            _listaTipoContatos = listaTipoContatos;
            _acoes = acoes;
            _fiis = fiis;
        }

        public async Task ProcessaPessoaAsync(PessoaJson pessoaJson)
        {
            await _service.BeginTransactionAsync();
            try
            {
                string[] valoresContatos = { pessoaJson.Email, pessoaJson.Telefone_fixo, pessoaJson.Celular };
                PessoaDto pessoaDto = PessoaDto.NovaPessoaDto(pessoaJson);

                Pessoa pessoa = await _service.PessoaService.CriarNovoAsync(pessoaDto);
                EnderecoDto enderecoDto = EnderecoDto.NovoEnderecoDto(pessoa: pessoa, pessoaJson: pessoaJson);
                List<Contato> contatos = Contato.ListaDeContatos(
                    pessoa: pessoa, 
                    tipoContatos: _listaTipoContatos, 
                    valoresContatos: valoresContatos);
                CarteiraDto carteiraDtoLote = CarteiraDto.NovaCarteiraDto(pessoa: pessoa, acoes: _acoes, fiis: _fiis);

                await _service.ContatoService.CriarEmLoteAsync(contatos);
                await _service.EnderecoService.CriarNovoAsync(entidade: enderecoDto);
                await _service.CarteiraService.CriarNovasCarteirasLote(carteiraDtoLote);
                _service.SaveChanges();
                await _service.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _service.RollbackTransactionAsync();
                throw;
            }
        }
    }
}