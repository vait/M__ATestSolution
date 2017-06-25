using System.IO;

namespace WordCounter.Infrastructure
{
    /// <summary>
    /// Настройки системы
    /// </summary>
    public sealed class ModulesConfiguration
    {
        /// <summary>
        /// Путь к файлу с текстом
        /// </summary>
        public string InputFilePath { get; private set; }

        /// <summary>
        /// Путь к файлу с выводом результатов
        /// </summary>
        public string OutputFilePath { get; private set; }

        /// <summary>
        /// Строка соединения с БД
        /// </summary>
        public string ConnectionString { get; private set; }

        /// <summary>
        /// Поток вывода для 
        /// </summary>
        public TextWriter OutputStream { get; internal set; }

        /// <summary>
        /// Конструктор настроек
        /// </summary>
        /// <param name="inputFilePath">Путь к файлу с текстом</param>
        /// <param name="outputFilePath">Путь к файлу с результатами</param>
        /// <param name="connectionString">Строка соединения</param>
        public ModulesConfiguration(string inputFilePath, string outputFilePath, string connectionString, TextWriter outputStream)
        {
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
            ConnectionString = connectionString;
            OutputStream = outputStream;
        }
    }
}