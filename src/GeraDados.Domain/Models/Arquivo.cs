using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeraDados.DataModel.Models
{
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
}
