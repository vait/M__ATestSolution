using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCounter.Interfaces;
using WordCounter.Infrastructure;
using WordCounter.Implementations.Processor;

namespace UnitTest
{
    [TestClass]
    public class TextProcessorTest
    {
        [TestMethod]
        public void TestSpaceTextProcessor()
        {
            var sourceText = "В настоящее время, по техническим причинам, отображение запрошенной Вами информации невозможно. Попробуйте позднее.";
            var wordsCount = 13;
            TextProcessingResult result;

            using (ITextProcessor proc = new SpaceTextProcessor())
            {
                result = proc.Process(sourceText);
            }

            Assert.AreEqual(wordsCount, result.WordsCount);
        }

        [TestMethod]
        public void TestSpaceTextProcessorDoubleSpaces()
        {
            var sourceText = "В  настоящее время,  по техническим  причинам, отображение запрошенной Вами  информации невозможно. Попробуйте  позднее.";
            var wordsCount = 13;
            TextProcessingResult result;

            using (ITextProcessor proc = new SpaceTextProcessor())
            {
                result = proc.Process(sourceText);
            }

            Assert.AreEqual(wordsCount, result.WordsCount);
        }

        [TestMethod]
        public void TestPunctuationTextProcessor()
        {
            var sourceText = "В  настоящее время,по техническим  причинам,отображение запрошенной Вами  информации невозможно.Попробуйте позднее.";
            var wordsCount = 13;
            TextProcessingResult result;

            using (ITextProcessor proc = new PunctuationTextProcessor())
            {
                result = proc.Process(sourceText);
            }

            Assert.AreEqual(wordsCount, result.WordsCount);
        }
    }
}
