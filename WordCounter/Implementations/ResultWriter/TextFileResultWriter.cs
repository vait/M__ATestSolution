using System.ComponentModel;
using System.IO;
using WordCounter.Infrastructure;
using WordCounter.Interfaces;

namespace WordCounter.Implementations.ResultWriter
{
    /// <summary>
    /// Класс вывода результатов в файл
    /// </summary>
    [DisplayName("File")]
    public sealed class TextFileResultWriter : IResultWriter
    {
        /// <summary>
        /// Путь к файлу
        /// </summary>
        private string path;

        /// <summary>
        /// Писателья
        /// </summary>
        private TextWriter writer;

        public TextFileResultWriter(string path)
        {
            this.path = path;
        }

        public TextFileResultWriter(ModulesConfiguration config)
            : this(config.OutputFilePath)
        {

        }

        /// <summary>
        /// Записывает результат обработки текста в файл
        /// </summary>
        /// <param name="textProcessingResult">Результат</param>
        /// <returns>Возвращает статус записи</returns>
        public Result Write(TextProcessingResult textProcessingResult)
        {
            var result = Result.Fail;

            if (File.Exists(this.path))
                File.Delete(this.path);

            using (writer = File.CreateText(this.path))
            {
                writer.Write(textProcessingResult.WordsCount);
                result = Result.Success;
            }

            return result;
        }

        #region IDisposable
        public void Dispose()
        {
            if (writer != null)
                writer.Dispose();
        }
        #endregion
    }
}