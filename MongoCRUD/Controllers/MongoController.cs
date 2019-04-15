using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using MongoCRUD.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace MongoCRUD.Controllers
{

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
        // OData example: /api/mongo?$select=Id,Value&$OrderBy=Timestamp&$Filter=Id lt 3
        [HttpGet]
        [EnableQuery()]
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
