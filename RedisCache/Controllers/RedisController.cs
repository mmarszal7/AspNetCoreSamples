using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RedisCache.Controllers
{
    public class Record
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDatabase RedisDatabase;

        public RedisController(IDatabase database)
        {
            RedisDatabase = database;
        }

        [HttpGet("{key}")]
        public Task<RedisValue> Get(string key)
        {
            return RedisDatabase.StringGetAsync(key);
        }

        [HttpPost()]
        public Task Set(Record record)
        {
            return RedisDatabase.StringSetAsync(record.Key, record.Value);
        }

        [HttpPost()]
        [Route("caching")]
        public async Task<string> LongRunningOperation(string key)
        {
            string result = (await RedisDatabase.StringGetAsync(key));
            if (result != null)
                return result;

            // Long running operation
            result = await Task.Run(() => { Thread.Sleep(5000); return "Some value: " + Guid.NewGuid(); });

            await RedisDatabase.StringSetAsync(key, result);
            return result;
        }

    }
}
