using System.Collections.Generic;
using WordCount.Backend.Models;

namespace WordCount.Backend.Contracts
{
    public interface IWordCountService
    {
        IEnumerable<WordCountItem> GetWordCounts(string input);
    }
}