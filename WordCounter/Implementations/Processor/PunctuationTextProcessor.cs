using System.ComponentModel;
using System.Linq;
using WordCounter.Infrastructure;
using WordCounter.Interfaces;

namespace WordCounter.Implementations.Processor
{
    /// <summary>
    /// Класс обработки текста простым разбиением на слова через пробелы
    /// двойные пробелы пропускаются, при этом знаки препинания заменяются
    /// на пробел
    /// </summary>
    [DisplayName("SpacesWithPunctuation")]
    public sealed class PunctuationTextProcessor : ITextProcessor
    {
        private string[] punctuationMarks = new[] {".", ",", "!", "?", "(", ")", "[", "]", ";", ":", "\"", "'", " -", "- ", " - " };

        public PunctuationTextProcessor()
        {

        }

        public PunctuationTextProcessor(ModulesConfiguration config)
            : this()
        {

        }

        /// <summary>
        /// Считает количество слов, разделенных пробелом. Знаки препинания заменяет на пробел
        /// </summary>
        /// <param name="text">Исходный текст</param>
        /// <returns>Результат пересчета</returns>
        public TextProcessingResult Process(string text)
        {
            var spaceProcessor = new SpaceTextProcessor();

            //Зачищаем знаки пунктуации
            punctuationMarks.AsParallel().ForAll((mark) => text = text.Replace(mark, " "));

            return spaceProcessor.Process(text);
        }

        #region IDisposable
        public void Dispose()
        {
        }
#endregion
    }
}