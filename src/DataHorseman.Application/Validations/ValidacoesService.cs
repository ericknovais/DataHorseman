using System.Diagnostics.CodeAnalysis;

namespace DataHorseman.Application.Validations
{
    public static class ValidacoesService
    {
        public static void EntidadeEhNula<T>(T entidade)
        {
            if (entidade is null)
                throw new ArgumentNullException(nameof(entidade), $"{typeof(T).Name} não pode ser nulo.");
        }

        public static void ValidaIdMaiorQueZero(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "O ID deve ser maior que zero.");
        }

        public static void ObjetoNaoEncontrado<T>([NotNull] T? objeto, int id, string nomeEntidade) where T : class
        {
            if (objeto is null)
                throw new KeyNotFoundException($"Nenhum {nomeEntidade} encontrado com o ID {id}.");
        }

        public static void ValidaListaNulaOuVazia<T>(IEnumerable<T> lista, string nomeLista)
        {
            if (lista is null || !lista.Any())
                throw new ArgumentNullException(nomeLista, $"A lista de {nomeLista} não pode ser nula ou vazia.");
        }
    }
}
