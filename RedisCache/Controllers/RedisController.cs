using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace RedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDatabase RedisDatabase;

        public RedisController(IDatabase database)
        {
            RedisDatabase = database;
        }

        [HttpGet("get/{key}")]
        public Task<RedisValue> Get(string key)
        {
            return RedisDatabase.StringGetAsync(key);
        }

        [HttpGet("set/{key}={value}")]
        public Task Set(string key, string value)
        {
            return RedisDatabase.StringSetAsync(key, value);
        }
    }
}
