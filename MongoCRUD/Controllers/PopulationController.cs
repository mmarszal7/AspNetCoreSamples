using AutoMapper;
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
        public List<ResponseDTO> Get()
        {
            var population = MongoDatabase.GetCollection<Population>("Population")
                .AsQueryable()
                .ToList();

            var response = Mapper.Map<List<Population>, List<ResponseDTO>>(population);
            return response;
        }
    }
}
