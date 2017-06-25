using Ninject.Modules;
using WordCounter.Interfaces.Ninject;
using WordCounter.Interfaces;
using WordCounter.Implementations.Processor;

namespace WordCounter.Infrastructure.Ninject.Modules
{
    public sealed class SpaceTextProcessor : NinjectModule, INamedType, ITextProcessorModule
    {
        private ModulesConfiguration config;

        public SpaceTextProcessor(ModulesConfiguration config)
            : base()
        {
            LocalName = "Spaces";
        }

        /// <summary>
        /// Локальное имя
        /// </summary>
        public string LocalName { get; }
        public override void Load()
        {
            Bind<ITextProcessor>().To<PunctuationTextProcessor>()
                .InTransientScope()
                .Named(LocalName);
        }
    }
}
