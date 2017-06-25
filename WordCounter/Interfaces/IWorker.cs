using System;
using System.Threading.Tasks;
using WordCounter.Infrastructure;

namespace WordCounter.Interfaces
{
    /// <summary>
    /// Непосредственно выполнитель работы (паттерн шаблонный метод)
    /// Делаем их принудительно асинхронными
    /// </summary>
    interface IWorker : IDisposable
    {
        Result Execute();

        Task<Result> ExecuteAsync();
    }
}
