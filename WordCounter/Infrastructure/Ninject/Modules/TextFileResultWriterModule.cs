using Ninject.Modules;
using WordCounter.Interfaces;
using WordCounter.Implementations.ResultWriter;
using WordCounter.Interfaces.Ninject;

namespace WordCounter.Infrastructure.Ninject.Modules
{
    public sealed class TextFileResultWriterModule : NinjectModule, INamedType, IResultWriterModule
    {
        private ModulesConfiguration config;

        public TextFileResultWriterModule(ModulesConfiguration config)
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
            Bind<IResultWriter>().To<TextFileResultWriter>()
                .InTransientScope()
                .Named(LocalName)
                .WithConstructorArgument(config.OutputFilePath);
        }
    }
}
