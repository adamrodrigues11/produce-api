using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Day02Exercises.Models
{
    public class ProduceSupplier
    {
        public int ProduceID { get; set; }

        public int SupplierID { get; set; }
        public int Qty { get; set; }

        // Navigation properties.
        // Parents.
        [JsonIgnore]
        public Produce? Produce { get; set; }
        [JsonIgnore]
        public Supplier? Supplier { get; set; }
    }

}
