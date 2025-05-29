using DataHorseman.Domain.Entidades;

namespace DataHorseman.Application.Dtos
{
    public class ContatosDto
    {
        public int ID { get; set; }
        public Pessoa Pessoa { get; set; } = new Pessoa();
        public TipoContato? TipoContato { get; set; } = new TipoContato();
        public string Valor { get; set; } = string.Empty;

        public static ContatosDto NovoContatoDto(Pessoa pessoa, TipoContato tipoContato, string valor)
        {
            return new ContatosDto
            {
                Pessoa = pessoa,
                TipoContato = tipoContato,
                Valor = valor
            };
        }
    }
}
