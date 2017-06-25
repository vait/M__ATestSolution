using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordCounter.Interfaces;
using WordCounter.Implementations.Source;
using WordCounter.Infrastructure;
using WordCounter.Implementations.ResultWriter;

namespace UnitTest
{
    [TestClass]
    public class TextFileTest
    {
        private ITextSource testTextFileSource;
        private IResultWriter testTextFileResultWriter;
        private string sourcePath;
        private string resultPath;

        [TestInitialize]
        public void Initialize()
        {
            sourcePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "FileSource.txt");
            testTextFileSource = new TextFileSource(sourcePath);

            resultPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Result.txt");
            testTextFileResultWriter = new TextFileResultWriter(resultPath);
        }

        [TestMethod]
        public void TestTextFileSource()
        {
            string expectedText = null;
            using (System.IO.TextReader reader = System.IO.File.OpenText(this.sourcePath))
            {
                expectedText = reader.ReadToEnd();
            }

            string actualText = this.testTextFileSource.GetText();

            Assert.AreEqual(expectedText, actualText);
        }

        [TestMethod]
        public void TestTextFileResultWriter()
        {
            var textProcessingResult = new TextProcessingResult
            {
                WordsCount = 120
            };

            //Записываем данные
            var result = this.testTextFileResultWriter.Write(textProcessingResult);

            Assert.AreEqual(Result.Success, result, "Ошибка при сохранении результата");

            string actualText = null;
            using (System.IO.TextReader reader = System.IO.File.OpenText(this.resultPath))
            {
                actualText = reader.ReadToEnd();
            }

            Assert.AreEqual(textProcessingResult.WordsCount.ToString(), actualText);
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (testTextFileSource != null)
                testTextFileSource.Dispose();
            if (testTextFileResultWriter != null)
                testTextFileSource.Dispose();
        }
    }
}
