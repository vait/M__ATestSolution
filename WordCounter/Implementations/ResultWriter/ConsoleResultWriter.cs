using System;
using System.ComponentModel;
using System.IO;
using WordCounter.Infrastructure;
using WordCounter.Interfaces;

namespace WordCounter.Implementations.ResultWriter
{
    /// <summary>
    /// Класс вывода результатов в консоль
    /// </summary>
    [DisplayName("Console")]
    public sealed class ConsoleResultWriter : IResultWriter
    {
        private TextWriter outputStream; 

        public ConsoleResultWriter(TextWriter outputStream)
        {
            this.outputStream = outputStream;
        }

        public ConsoleResultWriter(ModulesConfiguration config)
            : this(config.OutputStream)
        {

        }

        public Result Write(TextProcessingResult textProcessingResult)
        {
            outputStream.WriteLine("{1}{1}{1}This text has {0} word(s){1}", textProcessingResult.WordsCount, Environment.NewLine);

            return Result.Success;
        }

        #region IDisposable
        public void Dispose()
        {
        }
        #endregion
    }
}
