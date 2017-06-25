using WordCounter.Interfaces;

namespace WordCounter.Infrastructure.Ninject.Modules
{
    /// <summary>
    /// Модуль для загрузки обработчиков текста
    /// </summary>
    sealed class TextProcessorModule : AutoBindModule<ITextProcessor>
    {
        public TextProcessorModule(ModulesConfiguration config) 
            : base(config)
        {
        }

        public override void Load()
        {
            Bind<ITextProcessor>().To(necessaryType)
                .InTransientScope()
                .Named(necessaryTypeName);
        }
    }
}
