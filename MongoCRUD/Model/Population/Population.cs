using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MongoCRUD.Model
{
    public class Population
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string District { get; set; }
        public string Details { get; set; }
        [BsonElement("PopulationByYears")]
        [JsonProperty("PopulationByYears")]
        public Dictionary<string, int> Year { get; set; }
    }
}
