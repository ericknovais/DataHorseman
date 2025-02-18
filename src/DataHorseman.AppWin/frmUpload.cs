using DataHorseman.Application.Dtos;
using DataHorseman.Application.Interfaces;
using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Enums;
using DataHorseman.Infrastructure.Persistencia.Dtos;
using DataHorseman.Infrastructure.Persistencia.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DataHorseman.AppWin;

public partial class frmUpload : Form
{
    protected readonly Repository _repository = new Repository();
    protected readonly IService _service;
    private IList<TipoContato> _listaTipoContatos;
    private List<Ativo> _acoes;
    private List<Ativo> _fiis;

    #region M�todos Do Forms
    public frmUpload(IService service)
    {
        _acoes = new List<Ativo>();
        _fiis = new List<Ativo>(); 
        _listaTipoContatos = new List<TipoContato>();
        _service = service;
        InitializeComponent();   
    }
    private async void btnUpload_Click(object sender, EventArgs e)
    {
        await InicializaDadosNoBanco();
        await CarregaListas();
        //AtivoDto ativo = new AtivoDto();
        //var tiposDeAtivo = Enum.GetValues(typeof(AtivoDto))
        //               .Cast<AtivoDto>()
        //               .ToList();

        var teste = _service.AtivoService.ObtemAtivosPorTipoDeAtivoID(eTipoDeAtivo.Acao); 
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.CheckFileExists = true;
        ofd.Multiselect = false;
        if (ofd.ShowDialog() == DialogResult.OK)
            txtArquivo.Text = ofd.FileName;
    }
    private void btnSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            IList<PessoaJson>? pessoas = Arquivo.LerArquivoJson<PessoaJson>(txtArquivo.Text);

            if (pessoas != null && pessoas.Count > 0)
            {
                foreach (PessoaJson pessoaJson in pessoas)
                {
                    if (_repository.Pessoa.ObtemPessoaPorCPF(pessoaJson.CPF) != null)
                        continue;

                    string[] valoresContatos = { pessoaJson.Email, pessoaJson.Telefone_fixo, pessoaJson.Celular };
                    Pessoa pessoa = new Pessoa(pessoaJson.Nome, pessoaJson.CPF, pessoaJson.RG, pessoaJson.Sexo, Convert.ToDateTime(pessoaJson.Data_nasc));
                    List<Contato> contatos = Contato.ListaDeContatos(pessoa, _listaTipoContatos, valoresContatos);
                    Endereco endereco = new Endereco(pessoa, pessoaJson.CEP, pessoaJson.Endereco, pessoaJson.Numero, pessoaJson.Bairro, pessoaJson.Cidade, pessoaJson.Estado);
                    CarteiraConfigurada carteiraConfigurada = CarteiraConfigurada.NovaCarteiraConfiguracao(_acoes, _fiis);

                    _repository.Pessoa.CriarNovoAsync(pessoa);
                    contatos.ForEach(contato => _repository.Contato.CriarNovoAsync(contato));
                    _repository.Endereco.CriarNovoAsync(endereco);
                    carteiraConfigurada.Acoes.ForEach(
                        acao =>
                            _repository.Carteira.CriarNovoAsync(new Carteira(pessoa, acao, carteiraConfigurada.ValorParaAcoes))
                    );
                    carteiraConfigurada.Fiis.ForEach(
                        fii =>
                            _repository.Carteira.CriarNovoAsync(new Carteira(pessoa, fii, carteiraConfigurada.ValorPorFiis))
                    );
                    _repository.SaveChanges();
                }

                MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show($"O arquivo {Arquivo.ObtemNomeDoArquivo(txtArquivo.Text)} n�o contem dados!", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            txtArquivo.Text = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    #endregion

    #region M�todos void
    private async Task InicializaDadosNoBanco()
    {
        var tipoContatos = await _repository.TipoContato.ObterTodosAsync();
        if (tipoContatos.Count == 0)
            SalvaTipoDeContatosNoBanco();
        var tipoDeAtivos = await _repository.TipoDeAtivo.ObterTodosAsync();
        if (tipoDeAtivos.Count == 0)
            SalvaTipoDeAtivosNoBanco();
        if (_repository.Ativo.ObtemAtivosPorTipoDeAtivoID(eTipoDeAtivo.Acao).Count == 0)
            SalvarAtivosNoBancoDeDados(eTipoDeAtivo.Acao);
        if (_repository.Ativo.ObtemAtivosPorTipoDeAtivoID(eTipoDeAtivo.FundoImobiliario).Count == 0)
            SalvarAtivosNoBancoDeDados(eTipoDeAtivo.FundoImobiliario);
    }
    private async Task CarregaListas()
    {
        _listaTipoContatos = await _repository.TipoContato.ObterTodosAsync();
        _acoes = await ObtemListaDeAtivosPorTipoDeAtivo(eTipoDeAtivo.Acao);
        _fiis = await ObtemListaDeAtivosPorTipoDeAtivo(eTipoDeAtivo.FundoImobiliario);
    }
    private void SalvarAtivosNoBancoDeDados(eTipoDeAtivo tipoDeAtivo)
    {
        if (tipoDeAtivo != null)
            if (tipoDeAtivo == eTipoDeAtivo.Acao)
            {
                IList<AtivoJson>? acoes = Arquivo.LerArquivoJson<AtivoJson>(@"..\..\..\CargaDeAtivos\acoes.json");
                if (acoes != null)
                    SalvaListaDeAtivos(acoes.ToList(), tipoDeAtivo);
            }
            else if (tipoDeAtivo == eTipoDeAtivo.FundoImobiliario)
            {
                IList<AtivoJson>? fii = Arquivo.LerArquivoJson<AtivoJson>(@"..\..\..\CargaDeAtivos\fiis.json");
                if (fii != null)
                    SalvaListaDeAtivos(fii.Where(fii => fii.Ultimo != "0").ToList(), tipoDeAtivo);
            }
    }
    private void SalvaListaDeAtivos(List<AtivoJson> ativos, eTipoDeAtivo tipoDeAtivo)
    {
        ativos.ForEach(
                ativoJson =>
                    _repository.Ativo.CriarNovoAsync(

                        Ativo.NovoAtivo(
                            tipoDeAtivoID: tipoDeAtivo,
                            ticker: ativoJson.Ticker,
                            nome: ativoJson.Nome,
                            ultimaNegociacao: Convert.ToDecimal($"{ativoJson.Ultimo},{ativoJson.Decimal}")
                        )
                    )
        );
        _repository.SaveChanges();
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

    #region M�todos com return
    private async Task<List<Ativo>> ObtemListaDeAtivosPorTipoDeAtivo(eTipoDeAtivo idTipoAtivo)
    {
        return _repository.Ativo.ObtemAtivosPorTipoDeAtivoID(idTipoAtivo);
    }
    #endregion
}