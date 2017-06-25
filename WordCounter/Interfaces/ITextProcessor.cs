using System;
using WordCounter.Infrastructure;

namespace WordCounter.Interfaces
{
    /// <summary>
    /// Обработчик текста
    /// </summary>
    public interface ITextProcessor: IDisposable
    {
        /// <summary>
        /// Обработка текста
        /// </summary>
        /// <returns>результат обработки текста</returns>
        TextProcessingResult Process(string text);
    }
}
