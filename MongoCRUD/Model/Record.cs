using System;

namespace MongoCRUD.Model
{
    public class Record
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public DateTime Timestamp { get; set; }

        public Event Event { get; set; }
    }
}
