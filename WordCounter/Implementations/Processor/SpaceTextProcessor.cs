using System;
using System.ComponentModel;
using WordCounter.Infrastructure;
using WordCounter.Interfaces;

namespace WordCounter.Implementations.Processor
{
    /// <summary>
    /// Класс обработки текста простым разбиением на слова через пробелы
    /// двойные пробелы пропускаются
    /// </summary>
    [DisplayName("Spaces")]
    public sealed class SpaceTextProcessor : ITextProcessor
    {
        public SpaceTextProcessor()
        {

        }

        public SpaceTextProcessor(ModulesConfiguration config)
            : this()
        {

        }

        /// <summary>
        /// Считает количество слов, разделенных пробелом
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <returns>Результат пересчета</returns>
        public TextProcessingResult Process(string text)
        {
            var result = new TextProcessingResult();

            result.WordsCount = text.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length;

            return result;
        }

        #region IDisposable
        public void Dispose()
        {
        }
        #endregion
    }
}