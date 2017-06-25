using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCounter.Implementations.Worker;
using WordCounter.Infrastructure;
using WordCounter.Infrastructure.Ninject.Modules;
using WordCounter.Interfaces;
using WordCounter.Interfaces.Ninject;

namespace WordCounter
{
    class Program
    {
        private static IKernel kernel { get; set; }

        private static string[] sourses;

        private static string[] writers;

        private static string[] processors;

        private static ModulesConfiguration config;

#if version2
        //Классы все от абстрактного класса, можно в массив объединить
        //и потом обработать загрузку через цикл. А сообщение к пользователю
        //через атрибуты добавить. Полет фантазии :)
        private static TextSourceModule textSourceModule;

        private static ResultWriterModule resultWriterModule;

        private static TextProcessorModule textProcessorModule;
#endif

        static Program()
        {
            Initialize();
        }

        // инициализация
        /*
         * грузим все модули из сборки
         * выделяем источники, приемники, обработчики
         * делаем запрос с консоли, что делать
         */

        /// <summary>
        /// Инициализирует проект
        /// </summary>
        private static void Initialize()
        {
            //Настройки можно вынести как настройки приложения, так и сделать сериализуемыми
            config = new ModulesConfiguration(
                inputFilePath: System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "FileSource.txt"),
                outputFilePath: System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Result.txt"),
                connectionString: String.Empty,
                outputStream: Console.Out
            );

#if version1 //Более простой вариант, но требует создания модуля связки на каждый класс
            //Ищем модули для DI контейнера. 
            var modules = new List<INinjectModule>(15);

            //Также для расширяемости можно добавить загрузку сборок из внешних библиотек, не подключенных
            //к проекту

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                var assemblyModules = assembly.GetTypes()
                    .Where(t => t.GetInterfaces()
                        .Any(i => i == typeof(INamedType)))
                    .Select(t => Activator.CreateInstance(t, new object[] { config }) as INinjectModule)
                    .ToArray();
                modules.AddRange(assemblyModules);
            }

            //Здесь можно сделать проверку на наличие модулей с одинаковыми именами,
            //Так как в данном решении объекты создаются по имени связки

            kernel = new StandardKernel(modules.ToArray());

            //Выбираем все источники
            sourses = modules
                .Where(m => m.GetType().GetInterfaces().Any(i => i == typeof(ISourceModule)))
                .Select(m => (m as INamedType).LocalName)
                .ToArray();

            //Выбираем всех писателей
            writers = modules
                .Where(m => m.GetType().GetInterfaces().Any(i => i == typeof(IResultWriterModule)))
                .Select(m => (m as INamedType).LocalName)
                .ToArray();

            //Выбираем все обработчики текста
            processors = modules
                .Where(m => m.GetType().GetInterfaces().Any(i => i == typeof(ITextProcessorModule)))
                .Select(m => (m as INamedType).LocalName)
                .ToArray();
#endif

#if version2 //Более сложный вариант, но добавляющий больше гибкости: достаточно просто добавить класс 
            //в сборку и унаследовать его от нужного интерфейса. Но заставляет жестко 
            //связывать объекты исполнители с классом конфигурации
            textSourceModule = new TextSourceModule(config);
            textSourceModule.LoadAssebliesTypes();
            resultWriterModule = new ResultWriterModule(config);
            resultWriterModule.LoadAssebliesTypes();
            textProcessorModule = new TextProcessorModule(config);
            textProcessorModule.LoadAssebliesTypes();

            sourses = textSourceModule.AvailableTypeNames;
            writers = resultWriterModule.AvailableTypeNames;
            processors = textProcessorModule.AvailableTypeNames;
#endif
        }

        /// <summary>
        /// Предлагает выбрать пользователю один из вариантов
        /// </summary>
        /// <param name="message">Приглашение к выбору</param>
        /// <param name="moduleNames">варианты выбора</param>
        /// <returns>выбранный вариант</returns>
        private static string SelectModuleName(string message, string[] moduleNames)
        {
            Console.WriteLine(message);
            for (int i = 0; i < moduleNames.Length; ++i)
            {
                Console.WriteLine("{0}: {1}", i, moduleNames[i]);
            }

            string input = String.Empty;
            int index = -1;

            do
            {
                Console.Write("Your choice is: ");
                input = Console.ReadLine();

                if (!int.TryParse(input, out index))
                {
                    index = -1;
                    input = String.Empty;
                    Console.WriteLine("Wrong number");
                }
                else if (index < 0 || moduleNames.Length <= index)
                {
                    index = -1;
                    input = String.Empty;
                    Console.WriteLine("Out of index");
                }
            }
            while (input == String.Empty);

            Console.WriteLine();

            return moduleNames[index];
        }

        static void Main(string[] args)
        {
            Initialize();
            ITextSource textSource = null;
            IResultWriter resultWriter = null;
            ITextProcessor textProcessor = null;

#if version1
            //Проверяем наличие модулей
            if (sourses.Length == 0)
            {
                System.Diagnostics.Trace.WriteLine("Not found any SourceModule");
                return;
            }

            if (writers.Length == 0)
            {
                System.Diagnostics.Trace.WriteLine("Not found any SourceModule");
                return;
            }

            if (processors.Length == 0)
            {
                System.Diagnostics.Trace.WriteLine("Not found any SourceModule");
                return;
            }
#endif
            //Получаем название модулей и загружаем
            var textSourceName = SelectModuleName("Select a source of text", sourses);
            var resultWriterName = SelectModuleName("Select method of write result", writers);
            var textProcessorName = SelectModuleName("Select method of word processing", processors);

#if version2
            textSourceModule.SetNecessaryType(textSourceName);
            resultWriterModule.SetNecessaryType(resultWriterName);
            textProcessorModule.SetNecessaryType(textProcessorName);

            kernel = new StandardKernel(new NinjectModule[] { textSourceModule, resultWriterModule, textProcessorModule });
#endif

            textSource = kernel.Get<ITextSource>(textSourceName);
            resultWriter = kernel.Get<IResultWriter>(resultWriterName);
            textProcessor = kernel.Get<ITextProcessor>(textProcessorName);

            //Запускаем обработку текста
            var spinner = new ConsoleSpiner();
            spinner.Start("Processing");

            IWorker worker = new SimpleWorker(textSource, resultWriter, textProcessor);
            var d = worker.ExecuteAsync().Result;

            spinner.Stop();
            Console.WriteLine();
            Console.Write("All done. Press any key...");
            Console.ReadKey();
        }
    }
}
