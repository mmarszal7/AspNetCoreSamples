using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MongoCRUD.Controllers
{
    public class Record
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        public IMongoDatabase MongoDatabase { get; }

        public MongoController(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }

        // GET api/mongo
        [HttpGet]
        public ActionResult<IEnumerable<Record>> Get()
        {
            return MongoDatabase.GetCollection<Record>("Records")
                .AsQueryable()
                .ToList();
        }

        // GET api/mongo/5
        [HttpGet("{id}")]
        public ActionResult<Record> Get(int id)
        {
            return MongoDatabase.GetCollection<Record>("Records")
                .Find(e => e.Id.Equals(id))
                .FirstOrDefault();
        }

        // POST api/mongo
        [HttpPost]
        public void Post([FromBody] Record entity)
        {
            MongoDatabase.GetCollection<Record>("Records")
                .ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity, new UpdateOptions { IsUpsert = true });
        }

        // DELETE api/mongo/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            MongoDatabase.GetCollection<Record>("Records")
                .DeleteOneAsync(x => x.Id.Equals(id));
        }
    }
}
