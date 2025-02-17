using DataHorseman.Domain.Entidades;
using DataHorseman.Domain.Enums;
using DataHorseman.Infrastructure.Persistencia.Dtos;
using DataHorseman.Infrastructure.Persistencia.Repositories;
using System.Threading.Tasks;

namespace DataHorseman.AppWin;

public partial class frmUpload : Form
{
    Repository repository = new Repository();
    protected IList<TipoContato> listaTipoContatos;
    private List<Ativo> acoes;
    private List<Ativo> fiis;

    #region Métodos Do Forms
    public frmUpload()
    {
        InitializeComponent();
        Task task = InicializaDadosNoBanco();
        CarregaListas();
    }
    private void btnUpload_Click(object sender, EventArgs e)
    {
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
                    if (repository.Pessoa.ObtemPessoaPorCPF(pessoaJson.CPF) != null)
                        continue;

                    string[] valoresContatos = { pessoaJson.Email, pessoaJson.Telefone_fixo, pessoaJson.Celular };
                    Pessoa pessoa = new Pessoa(pessoaJson.Nome, pessoaJson.CPF, pessoaJson.RG, pessoaJson.Sexo, Convert.ToDateTime(pessoaJson.Data_nasc));
                    List<Contato> contatos = Contato.ListaDeContatos(pessoa, listaTipoContatos, valoresContatos);
                    Endereco endereco = new Endereco(pessoa, pessoaJson.CEP, pessoaJson.Endereco, pessoaJson.Numero, pessoaJson.Bairro, pessoaJson.Cidade, pessoaJson.Estado);
                    CarteiraConfigurada carteiraConfigurada = CarteiraConfigurada.NovaCarteiraConfiguracao(acoes, fiis);

                    repository.Pessoa.CriarNovoAsync(pessoa);
                    contatos.ForEach(contato => repository.Contato.CriarNovoAsync(contato));
                    repository.Endereco.CriarNovoAsync(endereco);
                    carteiraConfigurada.Acoes.ForEach(
                        acao =>
                            repository.Carteira.CriarNovoAsync(new Carteira(pessoa, acao, carteiraConfigurada.ValorParaAcoes))
                    );
                    carteiraConfigurada.Fiis.ForEach(
                        fii =>
                            repository.Carteira.CriarNovoAsync(new Carteira(pessoa, fii, carteiraConfigurada.ValorPorFiis))
                    );
                    repository.SaveChanges();
                }

                MessageBox.Show("Cadastro realizado com sucesso!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show($"O arquivo {Arquivo.ObtemNomeDoArquivo(txtArquivo.Text)} não contem dados!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            txtArquivo.Text = string.Empty;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    #endregion

    #region Métodos void
    private async Task InicializaDadosNoBanco()
    {
        var tipoContatos = await repository.TipoContato.ObterTodosAsync();
        if (tipoContatos.Count == 0)
            SalvaTipoDeContatosNoBanco();
        var tipoDeAtivos = await repository.TipoDeAtivo.ObterTodosAsync();
        if (tipoDeAtivos.Count == 0)
            SalvaTipoDeAtivosNoBanco();
        TipoDeAtivo? acao = await repository.TipoDeAtivo.ObterPorIdAsync((int)eTipoDeAtivo.Acao);

        if (repository.Ativo.ObtemAtivosPorTipoDeAtivo(acao).Count.Equals(0))
            SalvarAtivosNoBancoDeDados(acao);
        var fii = await repository.TipoDeAtivo.ObterPorIdAsync((int)eTipoDeAtivo.FundoImobiliario);
        if (repository.Ativo.ObtemAtivosPorTipoDeAtivo(fii).Count.Equals(0))
            SalvarAtivosNoBancoDeDados(fii);
    }
    private async Task CarregaListas()
    {
        listaTipoContatos = await repository.TipoContato.ObterTodosAsync();
        acoes = await ObtemListaDeAtivosPorTipoDeAtivo((int)eTipoDeAtivo.Acao);
        fiis = await ObtemListaDeAtivosPorTipoDeAtivo((int)eTipoDeAtivo.FundoImobiliario);
    }
    private void SalvarAtivosNoBancoDeDados(TipoDeAtivo? tipoDeAtivo)
    {
        if (tipoDeAtivo != null)
            if (tipoDeAtivo.ID.Equals((int)eTipoDeAtivo.Acao))
            {
                IList<AtivoJson>? acoes = Arquivo.LerArquivoJson<AtivoJson>(@"..\..\..\CargaDeAtivos\acoes.json");
                if (acoes != null)
                    SalvaListaDeAtivos(acoes.ToList(), tipoDeAtivo);
            }
            else if (tipoDeAtivo.ID.Equals((int)eTipoDeAtivo.FundoImobiliario))
            {
                IList<AtivoJson>? fii = Arquivo.LerArquivoJson<AtivoJson>(@"..\..\..\CargaDeAtivos\fiis.json");
                if (fii != null)
                    SalvaListaDeAtivos(fii.Where(fii => fii.Ultimo != "0").ToList(), tipoDeAtivo);
            }
    }
    private void SalvaListaDeAtivos(List<AtivoJson> ativos, TipoDeAtivo tipoDeAtivo)
    {
        ativos.ForEach(
                ativoJson =>
                    repository.Ativo.CriarNovoAsync(

                        Ativo.AdicionarNovoAtivo(
                            tipoDeAtivo: tipoDeAtivo,
                            ticker: ativoJson.Ticker,
                            nome: ativoJson.Nome,
                            ultimaNegociacao: Convert.ToDecimal($"{ativoJson.Ultimo},{ativoJson.Decimal}")
                        )
                    )
        );
        repository.SaveChanges();
    }
    private void SalvaTipoDeAtivosNoBanco()
    {
        List<TipoDeAtivo> listaTipoDeAtivos = new TipoDeAtivo().CarregaTipoDeAtivo();
        foreach (TipoDeAtivo tipoDeAtivo in listaTipoDeAtivos)
            repository.TipoDeAtivo.CriarNovoAsync(tipoDeAtivo);
        repository.SaveChanges();
    }
    private void SalvaTipoDeContatosNoBanco()
    {
        List<TipoContato> listadeTipocontatos = new TipoContato().CarregaListaTipoContato();
        foreach (TipoContato tipoContato in listadeTipocontatos)
            repository.TipoContato.CriarNovoAsync(tipoContato);
        repository.SaveChanges();
    }
    #endregion

    #region Métodos com return
    private async Task<List<Ativo>> ObtemListaDeAtivosPorTipoDeAtivo(int idTipoAtivo)
    {
        return repository.Ativo.ObtemAtivosPorTipoDeAtivo(await repository.TipoDeAtivo.ObterPorIdAsync(idTipoAtivo));
    }
    #endregion
}