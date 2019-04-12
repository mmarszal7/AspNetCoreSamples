using System.Collections.Generic;

namespace MongoCRUD.Model
{
    public class ResponseDTO
    {
        public string Area { get; set; }
        public Dictionary<string, int> Min { get; set; }
        public Dictionary<string, int> Max { get; set; }
        public Dictionary<string, int> Average { get; set; }
    }
}
