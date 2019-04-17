using System.Collections.Generic;

namespace MongoCRUD.Model
{
    public class ResponseDTO
    {
        public string Area { get; set; }
        public string Details { get; set; }
        public int Year1970 { get; set; }
        public int Year1980 { get; set; }
        public int Year1990 { get; set; }
        public int Year2000 { get; set; }
        public int Year2010 { get; set; }
        public int Average { get; set; }
        public Dictionary<string, int> Min { get; set; }
        public Dictionary<string, int> Max { get; set; }
    }
}
