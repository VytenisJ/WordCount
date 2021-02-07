using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WordCount.Backend.Contracts;
using WordCount.Backend.Models;

namespace WordCount.Backend.Services
{
    public class WordCountService : IWordCountService
    {
        public IEnumerable<WordCountItem> GetWordCounts(string input)
        {
            return Regex.Matches(input, @"\w+") // splits input into words by removing non word characters
                    .GroupBy(x => RemoveDiacritics(x.Value).ToLower()) // groups into words with removed diacritics
                    .OrderByDescending(x => x.Count())
                    .Select(x => new WordCountItem {
                        Word = x.First().Value,
                        Rate = x.Count()
                    });
        }

        private string RemoveDiacritics(string input)
        {
            return string.Concat(
                    input.Normalize(NormalizationForm.FormD) // decmposes string into form of 'character', 'diacritic'
				    .Where(ch => CharUnicodeInfo.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)) // selects only non diacritic characters
                .Normalize(NormalizationForm.FormC); // composes the string back
        }
    }
}