using System;
using WordCounter.Infrastructure;

namespace WordCounter.Interfaces
{
    /// <summary>
    /// Интерфейс записи результатов
    /// </summary>
    public interface IResultWriter : IDisposable
    {
        Result Write(TextProcessingResult result);
    }
}
