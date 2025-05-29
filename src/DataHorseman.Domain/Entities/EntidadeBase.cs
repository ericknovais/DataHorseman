using System.Text;

namespace DataHorseman.Domain.Entidades;

public abstract class EntidadeBase
{
    public int ID { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAtualizacao { get; set; }

    private readonly StringBuilder _msgErro = new();

    public virtual void Valida()
    {
        if (_msgErro.Length > 0)
            throw new Exception(_msgErro.ToString());
    }

    protected void LimparErros() => _msgErro.Clear();

    protected void AdicionarErro(string mensagem) => _msgErro.AppendLine(mensagem);

    protected void ValidaCampoTexto(string valorCampo, string nomeCampo)
    {
        if (string.IsNullOrWhiteSpace(valorCampo))
            _msgErro.AppendLine($"O campo {nomeCampo} é obrigatório!");
    }

    protected void ValidaCampoNumerico(int campoNumerico, string nomeCampo)
    {
        if (campoNumerico <= 0)
            _msgErro.AppendLine($"O campo {nomeCampo} deve ser maior que zero!");
    }

    protected void ValidaCamposObrigatorios(params (string Valor, string Nome)[] campos)
    {
        foreach (var (valor, nome) in campos)
        {
            if (string.IsNullOrWhiteSpace(valor))
                _msgErro.AppendLine($"O campo {nome} é obrigatório!");
        }
    }
}