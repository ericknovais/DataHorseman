using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataHorseman.Domain.Entities;

[NotMapped]
public class Arquivo 
{
    public static IList<T>? LerArquivoJson<T>(string caminho)
    {
        return JsonConvert.DeserializeObject<IList<T>>(LeitorDeArquivo(caminho));
    }
    public static string LeitorDeArquivo(string caminho)
    {
        StreamReader reader = new StreamReader(caminho);
        return reader.ReadToEnd();
    }
    public static string ObtemNomeDoArquivo(string caminhoArquivo)
    {
        return caminhoArquivo.Split(@"\").Last();
    }
}
