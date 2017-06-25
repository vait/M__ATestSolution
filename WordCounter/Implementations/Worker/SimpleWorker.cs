using System;
using System.Threading.Tasks;
using WordCounter.Infrastructure;
using WordCounter.Interfaces;

namespace WordCounter.Implementations.Worker
{
    class SimpleWorker: IWorker
    {
        /// <summary>
        /// Источник текста
        /// </summary>
        public ITextSource Source { get; private set; }

        /// <summary>
        /// Приемник результата
        /// </summary>
        public IResultWriter ResultWriter { get; private set; }

        /// <summary>
        /// Обработчик текста
        /// </summary>
        public ITextProcessor TextProcessor { get; private set; }

        public SimpleWorker(ITextSource source, IResultWriter resultWriter, ITextProcessor textProcessor)
        {
            Source = source;
            ResultWriter = resultWriter;
            TextProcessor = textProcessor;
        }

        /// <summary>
        /// Выполняет чтение, обработку текста и запись результатов
        /// </summary>
        /// <returns>Результат сохранения</returns>
        public Result Execute()
        {
            var text = Source.GetText();
            var processingResult = TextProcessor.Process(text);

            return ResultWriter.Write(processingResult);
        }

        /// <summary>
        /// Асинхронное выполнение чтениея, обработки текста и записи результатов
        /// </summary>
        /// <returns>Результат сохранения</returns>
        public async Task<Result> ExecuteAsync()
        {
            return await Task.Run<Result>(new Func<Result>(() => Execute()));
        }

        #region IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
