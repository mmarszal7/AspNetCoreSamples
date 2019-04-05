using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CachingController : ControllerBase
    {
        private readonly IDatabase RedisDatabase;
        private readonly IMemoryCache MemoryCache;

        public CachingController(IDatabase database, IMemoryCache memoryCache)
        {
            RedisDatabase = database;
            MemoryCache = memoryCache;
        }

        [HttpPost()]
        [Route("redisCaching")]
        public async Task<string> RedisCaching(string key)
        {
            string result = (await RedisDatabase.StringGetAsync(key));
            if (result != null)
                return result;

            result = await LongRunningOperation;

            await RedisDatabase.StringSetAsync(key, result);
            return result;
        }

        [HttpPost()]
        [Route("memoryCaching")]
        public async Task<string> MemoryCaching(string key)
        {
            var result = MemoryCache.Get<string>(key);
            if (result != null)
                return result;

            result = await LongRunningOperation;

            // Some caching options
            var cachingOptions = new MemoryCacheEntryOptions()
                .SetSize(1)
                .SetPriority(CacheItemPriority.Normal)
                .SetAbsoluteExpiration(DateTime.Now.AddDays(3))
                .SetSlidingExpiration(TimeSpan.FromDays(1))
                .RegisterPostEvictionCallback((object keys, object value, EvictionReason reason, object state) => Console.WriteLine($"Entry was evicted. Reason: {reason}."));

            MemoryCache.Set(key, result, cachingOptions);
            return result;
        }

        [HttpGet()]
        [Route("responseCaching")]
        public async Task<string> ClientSideCaching()
        {
            var result = await LongRunningOperation;
            return result;
        }

        private Task<string> LongRunningOperation => Task.Run(() => { Thread.Sleep(5000); return "Some value: " + Guid.NewGuid(); });
    }
}
