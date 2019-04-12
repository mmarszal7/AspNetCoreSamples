using System.ComponentModel.DataAnnotations.Schema;

namespace MongoCRUD.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Reason { get; set; }
    }
}
