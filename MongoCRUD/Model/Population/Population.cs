using System.Collections.Generic;

namespace MongoCRUD.Model
{
    public class Population
    {
        public string _id { get; set; }
        public string District { get; set; }
        public string Details { get; set; }
        public Dictionary<string, int> PopulationByYears { get; set; }
    }
}
