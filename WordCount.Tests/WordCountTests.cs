using System.Linq;
using NUnit.Framework;
using WordCount.Backend.Contracts;
using WordCount.Backend.Services;

namespace WordCount.Tests
{
    public class WordCountTests
    {
        private IWordCountService service;

        [SetUp]
        public void Setup()
        {
            service = new WordCountService();
        }

        [Test]
        public void GetWordCounts_EmptyInput()
        {
            var results = service.GetWordCounts(string.Empty);

            Assert.NotNull(results);
        }

        [Test]
        public void GetWordCounts_Input_NotEmpty()
        {
            var input = "“Ryga yra Latvijos sostinė. Rīga yra latvijos sostine. Sostinę aplanko daug turistų.“";

            var results = service.GetWordCounts(input);

            Assert.NotNull(results);
            Assert.AreEqual(results.Count(), 8);
            Assert.AreEqual(results.First().Word, "sostinė");
            Assert.AreEqual(results.First().Rate, 3);
        }
    }
}