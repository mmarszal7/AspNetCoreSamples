using AutoMapper;
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
    public class PopulationController : ControllerBase
    {
        public IMongoDatabase MongoDatabase { get; }

        public PopulationController(IMongoDatabase mongoDatabase)
        {
            MongoDatabase = mongoDatabase;
        }

        // GET api/population
        [HttpGet]
        [EnableQuery()]
        public ActionResult<IEnumerable<Population>> Get()
        {
            return MongoDatabase.GetCollection<Population>("Population")
                .AsQueryable()
                .ToList();
        }

        // POST api/population
        [HttpPost]
        public void Post([FromBody] Population entity)
        {
            MongoDatabase.GetCollection<Population>("Population")
                .ReplaceOneAsync(x => (x.District + x.Details).Equals((entity.District + entity.Details)), entity, new UpdateOptions { IsUpsert = true });
        }

        // POST api/populationForYear
        [HttpPost]
        public ResponseDTO GetPopulationForYear([FromBody] RequestDTO request)
        {
            var population = MongoDatabase.GetCollection<Population>("Population")
                .AsQueryable()
                .ToList();

            var response = Mapper.Map<ResponseDTO>(population);
            return response;
        }
    }
}
