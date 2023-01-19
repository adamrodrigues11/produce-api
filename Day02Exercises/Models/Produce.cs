using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Day02Exercises.Models
{
    public class Produce
    {
        public int ProduceID { get; set; }
        public string Description { get; set; }

        // Navigation properties.
        // Child.
        [JsonIgnore]
        public ICollection<ProduceSupplier>?
        ProduceSuppliers { get; set; }
    }

}
