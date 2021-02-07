using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WordCount.Backend.Contracts;
using WordCount.Backend.Models;

namespace WordCount.Backend.Controllers
{
    [Route("WordCount")]
    [ApiController]
    public class WordCountController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IWordCountService _wordCountService;

        public WordCountController(IMemoryCache cache, IWordCountService wordCountService)
        {
            _cache = cache;
            _wordCountService = wordCountService;
        }

        [HttpPost]
        public ActionResult Post(WordCountParams param)
        {
            // A very naive caching solution
            if (!_cache.TryGetValue(param.Text, out IEnumerable<WordCountItem> cacheEntry))
            {

                cacheEntry = _wordCountService.GetWordCounts(param.Text);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(12));

                _cache.Set(param.Text, cacheEntry, cacheEntryOptions);
            }

            return Ok(cacheEntry);
        }
    }
}
