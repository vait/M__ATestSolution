using Ninject.Modules;
using WordCounter.Interfaces;
using WordCounter.Implementations.Source;
using WordCounter.Interfaces.Ninject;

namespace WordCounter.Infrastructure.Ninject.Modules
{
    public sealed class TextFileSourceModule : NinjectModule, INamedType, ISourceModule
    {
        private ModulesConfiguration config;

        public TextFileSourceModule(ModulesConfiguration config)
            :base()
        {
            this.config = config;
            LocalName = "File";
        }

        /// <summary>
        /// Локальное имя
        /// </summary>
        public string LocalName { get; }

        public override void Load()
        {
            Bind<ITextSource>().To<TextFileSource>()
                .InTransientScope()
                .Named(LocalName)
                .WithConstructorArgument(config.InputFilePath);
        }
    }
}
