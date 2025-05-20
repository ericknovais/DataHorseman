using System.Text;

namespace DataHorseman.Domain.Entidades;

public abstract class EntidadeBase
{
    public int ID { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime DataAtualizacao { get; set; }

    protected StringBuilder _msgErro = new StringBuilder();

    public virtual void Valida()
    {
        if (_msgErro.Length > 0)
            throw new Exception(_msgErro.ToString());
    }

    protected void ValidaCampoTexto(string valorCampo, string nomeCampo)
    {
        if (string.IsNullOrEmpty(valorCampo))
            _msgErro.Append($"O campo {nomeCampo} é obrigatório! {Environment.NewLine}");
    }

    protected void ValidaCampoNumerico(int campoNumerico, string nomeCampo)
    {
        if (campoNumerico <= 0)
            _msgErro.Append($"O campo {nomeCampo} deve ser maior que zero!");
    }
}