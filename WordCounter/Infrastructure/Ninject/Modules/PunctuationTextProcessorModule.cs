using Ninject.Modules;
using WordCounter.Interfaces.Ninject;
using WordCounter.Interfaces;
using WordCounter.Implementations.Processor;

namespace WordCounter.Infrastructure.Ninject.Modules
{
    public sealed class PunctuationTextProcessorModule : NinjectModule, INamedType, ITextProcessorModule
    {
        public PunctuationTextProcessorModule(ModulesConfiguration config)
            :base()
        {
            LocalName = "SpacesWithPunctuation";
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
