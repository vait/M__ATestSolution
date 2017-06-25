namespace WordCounter.Infrastructure
{
    /// <summary>
    /// Результат обработки текста
    /// </summary>
    public sealed class TextProcessingResult
    {
        /// <summary>
        /// Количество слов
        /// </summary>
        public long WordsCount { get; set; }
    }
}