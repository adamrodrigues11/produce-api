using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Day02Exercises.Models
{
    public class Produce
    {
        [Key]
        public int ProduceID { get; set; }
        public string Description { get; set; }

        // Navigation properties.
        // Child.
        [JsonIgnore]
        public virtual ICollection<ProduceSupplier>
        ?ProduceSuppliers { get; set; }
    }

}
