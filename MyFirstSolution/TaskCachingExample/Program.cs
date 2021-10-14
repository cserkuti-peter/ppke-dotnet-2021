using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TaskCachingExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpCache = new HttGetWithCache();
            var text = await httpCache.GetCachedAsync("https://www.encosoftware.hu/");
            text = await httpCache.GetCachedAsync("https://www.encosoftware.hu/");
        }
    }

    class HttGetWithCache
    {
        private ConcurrentDictionary<string, string> cache = new ConcurrentDictionary<string, string>();

        public async Task<string> GetAsync(string url)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var text = await response.Content.ReadAsStringAsync();
                cache[url] = text;
                return text;
            }
        }

        public Task<string> GetCachedAsync(string url)
        {
            return cache.TryGetValue(url, out var text)
                ? Task.FromResult(text)
                : GetAsync(url);
        }
    }
}
