using System.ComponentModel;
using System.IO;
using WordCounter.Infrastructure;
using WordCounter.Interfaces;

namespace WordCounter.Implementations.Source
{
    /// <summary>
    /// Источник текста из файла
    /// </summary>
    [DisplayName("File")]
    public sealed class TextFileSource : ITextSource
    {
        /// <summary>
        /// Путь к файлу
        /// </summary>
        private string path;

        /// <summary>
        /// читатель
        /// </summary>
        private TextReader reader;

        /// <summary>
        /// Источник текста из файла
        /// </summary>
        /// <param name="path">Путь к файлу с текстом</param>
        public TextFileSource(string path)
        {
            this.path = path;
            reader = null;
        }

        public TextFileSource(ModulesConfiguration config)
            : this(config.InputFilePath)
        {

        }

        /// <summary>
        /// Возвращает содержимое файла в виде строки
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            using(reader = File.OpenText(this.path))
            {
                return reader.ReadToEnd();
            }
        }

        #region IDisposable
        public void Dispose()
        {
            if (reader != null)
                reader.Dispose();
        }
        #endregion
    }
}