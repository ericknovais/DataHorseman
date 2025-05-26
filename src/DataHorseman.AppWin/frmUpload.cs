using AutoMapper;
using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Enums;
using DataHorseman.Infrastructure.Persistencia.Dtos;
using DataHorseman.Infrastructure.Persistencia.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataHorseman.AppWin;

public partial class frmUpload : Form
{
    protected readonly Repository _repository = new Repository();
    protected readonly IService _service;
    private IList<TipoContato> _listaTipoContatos;
    private List<AtivoDto> _acoes;
    private List<AtivoDto> _fiis;
    protected readonly IMapper _mapper;

    #region Métodos Do Forms
    public frmUpload(IService service)
    {
        _acoes = new List<AtivoDto>();
        _fiis = new List<AtivoDto>();
        _listaTipoContatos = new List<TipoContato>();
        _service = service;
        InitializeComponent();
    }
    private async void btnUpload_Click(object sender, EventArgs e)
    {
        await InicializaDadosNoBanco();
        await CarregaListas();
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.CheckFileExists = true;
        ofd.Multiselect = false;
        if (ofd.ShowDialog() == DialogResult.OK)
            txtArquivo.Text = ofd.FileName;
    }

    private IList<PessoaJson> CarregaPessoasDoArquivo()
    {
        var pessoas = _service.ArquivoService.LerArquivoJson<PessoaJson>(txtArquivo.Text);
        if (pessoas == null || !pessoas.Any())
            throw new Exception($"O arquivo {_service.ArquivoService.ObtemNomeDoArquivo(txtArquivo.Text)} não contem dados!");

        return pessoas;
    }

    private async void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            IList<PessoaJson> pessoas = CarregaPessoasDoArquivo();
            var pessoasFiltradas = ObterSomentePessoasNaoCadastradas(pessoas);
            var pessoasComErro = new List<string>();

            foreach (PessoaJson pessoaJson in pessoasFiltradas)
            {
                try
                {
                    await ProcessarPessoaAsync(pessoaJson);
                }
                catch (Exception)
                {
                    pessoasComErro.Add(pessoaJson.Nome);
                }
            }
            ExibirMensagemCadastro(pessoasComErro);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        finally
        {
            txtArquivo.Text = string.Empty;
        }
    }
    private void ExibirMensagemCadastro(List<string> pessoasComErro)
    {
        if (pessoasComErro.Any())
        {
            string mensagemErro = "As seguintes pessoas não foram cadastradas:\n" + string.Join("\n", pessoasComErro);
            MessageBox.Show(mensagemErro, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        else
        {
            MessageBox.Show("Todas as pessoas foram cadastradas com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    private async Task ProcessarPessoaAsync(PessoaJson pessoaJson)
    {
        await _service.BeginTransactionAsync();
        try
        {
            string[] valoresContatos = { pessoaJson.Email, pessoaJson.Telefone_fixo, pessoaJson.Celular };
            PessoaDto pessoaDto = PessoaDto.NovaPessoaDto(
                nome: pessoaJson.Nome,
                cpf: pessoaJson.CPF,
                rg: pessoaJson.RG,
                dataNascimento: Convert.ToDateTime(pessoaJson.Data_nasc),
                sexo: pessoaJson.Sexo
            );

            //List<Contato> contatos = Contato.ListaDeContatos(pessoa, _listaTipoContatos, valoresContatos);
            //Endereco endereco = new Endereco(pessoa, pessoaJson.CEP, pessoaJson.Endereco, pessoaJson.Numero, pessoaJson.Bairro, pessoaJson.Cidade, pessoaJson.Estado);
            pessoaDto.ID = await _service.PessoaService.CriarNovoAsync(pessoaDto);
            var pessoa = await _service.PessoaService.ObtemPessoaPorCPF(pessoaDto.CPF);
            CarteiraDto carteiraDtoLote = CarteiraDto.NovaCarteiraDto(pessoa, _acoes, _fiis);


            //contatos.ForEach(contato => _repository.Contato.CriarNovoAsync(contato));
            //await _repository.Contato.CriarEmLoteAsync(contatos);
            //await _repository.Endereco.CriarNovoAsync(endereco);

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
    #endregion

    #region Métodos void
    private async Task InicializaDadosNoBanco()
    {
        var tipoContatos = await _repository.TipoContato.ObterTodosAsync();
        if (!tipoContatos.Any())
            SalvaTipoDeContatosNoBanco();

        var tipoDeAtivos = await _repository.TipoDeAtivo.ObterTodosAsync();
        if (!tipoDeAtivos.Any())
            SalvaTipoDeAtivosNoBanco();

        var acoes = await _service.AtivoService.ObtemAtivosPorTipoDeAtivoID(eTipoDeAtivo.Acao);
        if (!acoes.Any())
            await SalvarAtivosNoBancoDeDados(eTipoDeAtivo.Acao);

        var fiis = await _service.AtivoService.ObtemAtivosPorTipoDeAtivoID(eTipoDeAtivo.FundoImobiliario);
        if (!fiis.Any())
            await SalvarAtivosNoBancoDeDados(eTipoDeAtivo.FundoImobiliario);
    }
    private async Task CarregaListas()
    {
        _listaTipoContatos = await _repository.TipoContato.ObterTodosAsync();
        _acoes = await ObtemListaDeAtivosPorTipoDeAtivo(eTipoDeAtivo.Acao);
        _fiis = await ObtemListaDeAtivosPorTipoDeAtivo(eTipoDeAtivo.FundoImobiliario);
    }
    private async Task SalvarAtivosNoBancoDeDados(eTipoDeAtivo tipoDeAtivo)
    {
        if (tipoDeAtivo == eTipoDeAtivo.Acao)
        {
            IList<AtivoJson>? acoes = _service.ArquivoService.LerArquivoJson<AtivoJson>(@"..\..\..\CargaDeAtivos\acoes.json");
            if (acoes != null)
                await SalvaListaDeAtivos(acoes.ToList(), tipoDeAtivo);
        }
        else if (tipoDeAtivo == eTipoDeAtivo.FundoImobiliario)
        {
            IList<AtivoJson>? fii = _service.ArquivoService.LerArquivoJson<AtivoJson>(@"..\..\..\CargaDeAtivos\fiis.json");
            if (fii != null)
                await SalvaListaDeAtivos(fii.Where(fii => fii.Ultimo != "0").ToList(), tipoDeAtivo);
        }
    }
    private async Task SalvaListaDeAtivos(List<AtivoJson> ativos, eTipoDeAtivo tipoDeAtivo)
    {
        var ativosDTO = ativos.Select(ativoJson => new AtivoDto
        {
            TipoDeAtivoId = tipoDeAtivo,
            Ticker = ativoJson.Ticker,
            Nome = ativoJson.Nome,
            UltimaNegociacao = Convert.ToDecimal($"{ativoJson.Ultimo},{ativoJson.Decimal}")
        }).ToList();

        await _service.AtivoService.CriarEmLoteAsync(ativosDTO);
    }
    private void SalvaTipoDeAtivosNoBanco()
    {
        List<TipoDeAtivo> listaTipoDeAtivos = new TipoDeAtivo().CarregaTipoDeAtivo();
        foreach (TipoDeAtivo tipoDeAtivo in listaTipoDeAtivos)
            _repository.TipoDeAtivo.CriarNovoAsync(tipoDeAtivo);
        _repository.SaveChanges();
    }
    private void SalvaTipoDeContatosNoBanco()
    {
        List<TipoContato> listadeTipocontatos = new TipoContato().CarregaListaTipoContato();
        foreach (TipoContato tipoContato in listadeTipocontatos)
            _repository.TipoContato.CriarNovoAsync(tipoContato);
        _repository.SaveChanges();
    }
    #endregion

    #region Métodos com return
    private async Task<List<AtivoDto>> ObtemListaDeAtivosPorTipoDeAtivo(eTipoDeAtivo idTipoAtivo)
    {
        return await _service.AtivoService.ObtemAtivosPorTipoDeAtivoID(idTipoAtivo);
    }

    public IList<PessoaJson> ObterSomentePessoasNaoCadastradas(IList<PessoaJson> pessoas)
    {
        if (pessoas is null || !pessoas.Any())
            return new List<PessoaJson>();

        var cpfs = pessoas.Select(p => p.CPF).ToList();
        var pessoasJaCadastradas = _service.PessoaService.VerificaSePessoasJaCadastradas(cpfs);

        if (pessoasJaCadastradas.Any())
        {
            var cpfsJaCadastrados = pessoasJaCadastradas
                .Select(p => p.CPF)
                .ToHashSet(); // mais rápido para busca

            pessoas = pessoas
                .Where(p => !cpfsJaCadastrados.Contains(p.CPF))
                .ToList();
        }

        return pessoas;
    }
    #endregion
}