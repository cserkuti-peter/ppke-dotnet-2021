using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomTaskCombinatorExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                var response = await RetryOnFault(() => client.GetAsync("https://www.encosoftware.hu"), 5);
            }
        }

        public static async Task<T> RetryOnFault<T>(Func<Task<T>> function, int maxTries)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try { return await function(); }
                catch { if (i == maxTries - 1) throw; }
            }
            return default(T);
        }

    }
}
