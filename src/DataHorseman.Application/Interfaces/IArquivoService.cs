namespace DataHorseman.Application.Interfaces
{
    public interface IArquivoService
    {

        /// <summary>
        /// Lê o conteúdo de um arquivo e retorna como uma string.
        /// </summary>
        /// <param name="caminho">Caminho completo do arquivo.</param>
        /// <returns>Conteúdo do arquivo como string.</returns>
        /// <exception cref="IOException">Lançado se houver erro ao ler o arquivo.</exception>
        IList<T>? LerArquivoJson<T>(string caminho);

        /// <summary>
        /// Lê um arquivo JSON e desserializa para uma lista de objetos do tipo T.
        /// </summary>
        /// <param name="caminho">Caminho do arquivo JSON.</param>
        /// <typeparam name="T">Tipo do objeto a ser desserializado.</typeparam>
        /// <returns>Lista de objetos do tipo T.</returns>
        /// <exception cref="InvalidOperationException">Lançado se houver erro ao desserializar o JSON.</exception>
        string LeitorDeArquivo(string caminho);

        /// <summary>
        /// Retorna o nome do arquivo sem o caminho completo.
        /// </summary>
        /// <param name="caminhoArquivo">Caminho completo do arquivo.</param>
        /// <returns>Nome do arquivo.</returns>
        string ObtemNomeDoArquivo(string caminhoArquivo);
    }
}