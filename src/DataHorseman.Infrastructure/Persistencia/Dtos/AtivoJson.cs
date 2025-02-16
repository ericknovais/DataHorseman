namespace DataHorseman.Infrastructure.Persistencia.Dtos
{
    public class AtivoJson
    {
        public AtivoJson()
        {
            Ticker = string.Empty;
            Nome = string.Empty;
            Ultimo = string.Empty;
            Decimal = string.Empty;
        }
        public string Ticker { get; set; }
        public string Nome { get; set; }
        public string Ultimo { get; set; }
        public string Decimal { get; set; }
    }
}
