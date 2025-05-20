using DataHorseman.Application.Interfaces;
using Newtonsoft.Json;

namespace DataHorseman.Application.Services;

/// <summary>
/// Serviço responsável pela leitura e manipulação de arquivos.
/// </summary>
public class ArquivoService : IArquivoService
{
    /// <summary>
    /// Lê o conteúdo de um arquivo e retorna como uma string.
    /// </summary>
    /// <param name="caminho">Caminho completo do arquivo.</param>
    /// <returns>Conteúdo do arquivo como string.</returns>
    /// <exception cref="IOException">Lançado se houver erro ao ler o arquivo.</exception>
    public string LeitorDeArquivo(string caminho)
    {
        try
        {
            using (StreamReader reader = new StreamReader(caminho))
            {
                return reader.ReadToEnd();
            }
        }
        catch (Exception ex)
        {
            throw new IOException($"Erro ao ler o arquivo {ObtemNomeDoArquivo(caminho)}.", ex);
        }
    }

    /// <summary>
    /// Lê um arquivo JSON e desserializa para uma lista de objetos do tipo T.
    /// </summary>
    /// <param name="caminho">Caminho do arquivo JSON.</param>
    /// <typeparam name="T">Tipo do objeto a ser desserializado.</typeparam>
    /// <returns>Lista de objetos do tipo T.</returns>
    /// <exception cref="InvalidOperationException">Lançado se houver erro ao desserializar o JSON.</exception>
    public IList<T>? LerArquivoJson<T>(string caminho)
    {
        try
        {
            string conteudo = LeitorDeArquivo(caminho);
            return JsonConvert.DeserializeObject<IList<T>>(conteudo);
        }
        catch (Exception ex)
        {
            // Log ou tratamento de erro
            throw new InvalidOperationException("Erro ao ler o arquivo JSON.", ex);
        }
    }

    /// <summary>
    /// Retorna o nome do arquivo sem o caminho completo.
    /// </summary>
    /// <param name="caminhoArquivo">Caminho completo do arquivo.</param>
    /// <returns>Nome do arquivo.</returns>
    public string ObtemNomeDoArquivo(string caminhoArquivo)
    {
        return Path.GetFileName(caminhoArquivo);
    }
}