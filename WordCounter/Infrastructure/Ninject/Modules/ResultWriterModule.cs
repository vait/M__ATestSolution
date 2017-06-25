using WordCounter.Interfaces;

namespace WordCounter.Infrastructure.Ninject.Modules
{
    /// <summary>
    /// Модуль для загрузки писателей результатов 
    /// </summary>
    /// <remarks>
    /// Можно объединить с источниками текста, но для данной задачи лучше так
    /// </remarks>
    class ResultWriterModule : AutoBindModule<IResultWriter>
    {
        public ResultWriterModule(ModulesConfiguration config) : base(config)
        {
        }

        public override void Load()
        {
            Bind<IResultWriter>().To(necessaryType)
                .InTransientScope()
                .Named(necessaryTypeName)
                .WithConstructorArgument(config);
        }
    }
}
