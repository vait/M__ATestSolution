using WordCounter.Interfaces;

namespace WordCounter.Infrastructure.Ninject.Modules
{
    /// <summary>
    /// Модуль для загрузки источников текста
    /// </summary>
    sealed class TextSourceModule : AutoBindModule<ITextSource>
    {
        public TextSourceModule(ModulesConfiguration config) : base(config)
        {
        }

        public override void Load()
        {
            Bind<ITextSource>().To(necessaryType)
                .InTransientScope()
                .Named(necessaryTypeName)
                .WithConstructorArgument(config);
        }
    }
}
