using System.Collections.Generic;

namespace MongoCRUD.Model
{
    public class Population
    {
        public string District { get; set; }
        public string Details { get; set; }
        public List<int> PopulationByYears { get; set; }
    }
}
