using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
using DataHorseman.Application.Mappers;
using DataHorseman.Application.UseCases.CadastrarPessoa;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Enums;
using DataHorseman.Infrastructure.Persistencia.Dtos;
using DataHorseman.Infrastructure.Persistencia.Repositories;
using System.Globalization;

namespace DataHorseman.AppWin;

public partial class frmUpload : Form
{
    protected readonly IService _service;
    private IList<TipoContato> _listaTipoContatos;
    private List<AtivoDto> _acoes;
    private List<AtivoDto> _fiis;

    #region Métodos Do Forms
    public frmUpload(IService service)
    {
        _acoes = new List<AtivoDto>();
        _fiis = new List<AtivoDto>();
        _listaTipoContatos = new List<TipoContato>();
        _service = service;
        InitializeComponent();
    }
    private void btnUpload_Click(object sender, EventArgs e)
    {

        OpenFileDialog ofd = new OpenFileDialog();
        ofd.CheckFileExists = true;
        ofd.Multiselect = false;
        if (ofd.ShowDialog() == DialogResult.OK)
            txtArquivo.Text = ofd.FileName;
    }
    private async void btnSalvar_Click(object sender, EventArgs e)
    {
        await SalvarAsync();
    }
    private async void frmUpload_Load(object sender, EventArgs e)
    {
        try
        {
            await InicializaDadosNoBanco();
            await CarregaListasAsync();
        }
        catch (Exception ex)
        {
            MensagemDeErro($"Erro ao carregar o formulário: {ex.Message}");
        }
    }
    #endregion

    #region Métodos asyncs
    private async Task InicializaDadosNoBanco()
    {
        try
        {
            var tipoContatos = await _service.TipoContatoService.ObterTodosAsync();
            if (!tipoContatos.Any())
                SalvaTipoDeContatosNoBanco();

            var tipoDeAtivos = await _service.TipoDeAtivoService.ObterTodosAsync();
            if (!tipoDeAtivos.Any())
                SalvaTipoDeAtivosNoBanco();

            var acoes = await _service.AtivoService.ObtemAtivosPorTipoDeAtivoIDAsync(eTipoDeAtivo.Acao);
            if (!acoes.Any())
                await SalvarAtivosNoBancoDeDados(eTipoDeAtivo.Acao);

            var fiis = await _service.AtivoService.ObtemAtivosPorTipoDeAtivoIDAsync(eTipoDeAtivo.FundoImobiliario);
            if (!fiis.Any())
                await SalvarAtivosNoBancoDeDados(eTipoDeAtivo.FundoImobiliario);
        }
        catch (Exception ex)
        {
            MensagemDeErro(ex.Message);
        }
    }
    private async Task CarregaListasAsync()
    {
        try
        {
            _listaTipoContatos = await _service.TipoContatoService.ObterTodosAsync();
            _acoes = await ObtemListaDeAtivosPorTipoDeAtivoAsync(eTipoDeAtivo.Acao);
            _fiis = await ObtemListaDeAtivosPorTipoDeAtivoAsync(eTipoDeAtivo.FundoImobiliario);
        }
        catch (Exception ex)
        {
            // Lidar com o erro adequadamente
            MensagemDeErro($"Ocorreu um erro ao carregar as listas: {ex.Message}");
        }
    }
    private async Task SalvarAtivosNoBancoDeDados(eTipoDeAtivo tipoDeAtivo)
    {
        string caminhoArquivo = tipoDeAtivo switch
        {
            eTipoDeAtivo.Acao => @"..\..\..\CargaDeAtivos\acoes.json",
            eTipoDeAtivo.FundoImobiliario => @"..\..\..\CargaDeAtivos\fiis.json",
            _ => throw new ArgumentOutOfRangeException(nameof(tipoDeAtivo), "Tipo de ativo não suportado.")
        };

        IList<AtivoJson>? ativos = _service.ArquivoService.LerArquivoJson<AtivoJson>(caminhoArquivo);
        if (ativos != null)
        {
            // Filtro específico para FIIs
            if (tipoDeAtivo == eTipoDeAtivo.FundoImobiliario)
                ativos = ativos.Where(a => a.Ultimo != "0").ToList();

            await SalvaListaDeAtivos(ativos.ToList(), tipoDeAtivo);
        }
    }
    private async Task SalvaListaDeAtivos(List<AtivoJson> ativos, eTipoDeAtivo tipoDeAtivo)
    {
        var cultura = new CultureInfo("pt-BR");

        var ativosDTO = ativos.Select(ativoJson =>
        {
            decimal valor = 0;
            decimal.TryParse($"{ativoJson.Ultimo},{ativoJson.Decimal}", NumberStyles.Any, cultura, out valor);
            return new AtivoDto
            {
                TipoDeAtivoId = tipoDeAtivo,
                Ticker = ativoJson.Ticker,
                Nome = ativoJson.Nome,
                UltimaNegociacao = valor
            };
        }).ToList();
        await _service.AtivoService.CriarEmLoteAsync(ativosDTO);
    }
    private async Task SalvarAsync()
    {
        try
        {
            IList<PessoaJson> pessoas = CarregaPessoasDoArquivo();
            IList<PessoaJson> pessoasFiltradas = await FiltraSomentePessoasNaoCadastradas(pessoas);
            List<string> pessoasComErro = await ProcessarPessoasAsync(pessoasFiltradas);
            ExibirMensagemCadastro(pessoasComErro);
        }
        catch (Exception ex)
        {
            MensagemDeErro(ex.Message);
        }
        finally
        {
            txtArquivo.Text = string.Empty;
        }
    }
    private async Task<List<string>> ProcessarPessoasAsync(IList<PessoaJson> pessoasFiltradas)
    {
        var pessoasComErro = new List<string>();
        var cadastroPessoaUseCase = new CadastroPessoaUseCase(_service, _listaTipoContatos, _acoes, _fiis);
        foreach (PessoaJson pessoaJson in pessoasFiltradas)
        {
            try
            {
              await cadastroPessoaUseCase.ProcessaPessoaAsync(pessoaJson);
            }
            catch (Exception)
            {
                pessoasComErro.Add(pessoaJson.Nome);
            }
        }
        return pessoasComErro;
    }
    #endregion

    #region Métodos sem returno
    private void SalvaTipoDeAtivosNoBanco()
    {
        List<TipoDeAtivo> listaTipoDeAtivos = TipoDeAtivo.CarregaTipoDeAtivo();
        foreach (TipoDeAtivo tipoDeAtivo in listaTipoDeAtivos)
            _service.TipoDeAtivoService.CriarNovoAsync(tipoDeAtivo);
        _service.SaveChanges();
    }
    private void SalvaTipoDeContatosNoBanco()
    {
        List<TipoContato> listadeTipocontatos = new TipoContato().CarregaListaTipoContato();
        foreach (TipoContato tipoContato in listadeTipocontatos)
            _service.TipoContatoService.CriarNovoAsync(tipoContato);
        _service.SaveChanges();
    }
    private void ExibirMensagemCadastro(List<string> pessoasComErro)
    {
        if (pessoasComErro.Any())
        {
            string mensagemErro = $"As seguintes pessoas não foram cadastradas:{Environment.NewLine}{string.Join(Environment.NewLine, pessoasComErro)}";
            MensagemDeErro(mensagemErro);
        }
        else
            MessageBox.Show("Todas as pessoas foram cadastradas com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    private void MensagemDeErro(string mensagem)
    {
        MessageBox.Show(mensagem, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    #endregion

    #region Métodos com return
    private async Task<List<AtivoDto>> ObtemListaDeAtivosPorTipoDeAtivoAsync(eTipoDeAtivo idTipoAtivo)
    {
        return await _service.AtivoService.ObtemAtivosPorTipoDeAtivoIDAsync(idTipoAtivo);
    }
    private IList<PessoaJson> CarregaPessoasDoArquivo()
    {
        var pessoas = _service.ArquivoService.LerArquivoJson<PessoaJson>(txtArquivo.Text);
        if (pessoas == null || !pessoas.Any())
            throw new Exception($"O arquivo {_service.ArquivoService.ObtemNomeDoArquivo(txtArquivo.Text)} não contem dados!");
        return pessoas;
    }
    public async Task<IList<PessoaJson>> FiltraSomentePessoasNaoCadastradas(IList<PessoaJson> pessoasJson)
    {
        if (pessoasJson is null || !pessoasJson.Any())
            return new List<PessoaJson>();

        var pessoasMapeadas = PessoaJsonMapper.MapearParaEntidadesValidas(pessoasJson);
        var pessoas = pessoasMapeadas.Select(pm => pm.Pessoa).ToList();
        var pessoasNaoCadastradas = await _service.PessoaService.FiltrarPessoasNaoCadastradas(pessoas);
        var cpfsNaoCadastrados = new HashSet<string>(pessoasNaoCadastradas.Select(p => p.CPF));

        // mapeia de volta pra PessoaJson, se precisar
        return pessoasMapeadas
        .Where(p => cpfsNaoCadastrados.Contains(p.Pessoa.CPF))
        .Select(p => p.PessoaJson)
        .ToList();
    }
    #endregion
}