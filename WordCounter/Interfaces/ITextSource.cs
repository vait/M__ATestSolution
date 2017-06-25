
using System;

namespace WordCounter.Interfaces
{
    /// <summary>
    /// Интерфейс источника текста
    /// </summary>
    public interface ITextSource: IDisposable
    {
        string GetText();
    }
}
