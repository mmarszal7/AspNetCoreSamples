using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System;
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

        [HttpPost()]
        public Task Set(Record record)
        {
            return RedisDatabase.StringSetAsync(record.Key, record.Value, expiry: new TimeSpan(1, 0, 0, 0));
        }

        [HttpGet("{key}")]
        public Task<RedisValue> Get(string key)
        {
            return RedisDatabase.StringGetAsync(key);
        }
    }
}
