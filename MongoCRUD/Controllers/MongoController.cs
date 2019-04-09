﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MongoCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoController : ControllerBase
    {
        // GET api/mongo
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/mongo/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/mongo
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/mongo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/mongo/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
