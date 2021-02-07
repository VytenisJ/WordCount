using System.ComponentModel.DataAnnotations;

namespace WordCount.Backend.Models
{
    public class WordCountParams
    {
        [StringLength(250)]
        public string Text { get; set; }
    }
}