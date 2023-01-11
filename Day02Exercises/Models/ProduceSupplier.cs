using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Day02Exercises.Models
{
    public class ProduceSupplier
    {
        [Key, Column(Order = 0)]
        public int ProduceID { get; set; }
        [Key, Column(Order = 1)]
        public int SupplierID { get; set; }
        public int Qty { get; set; }

        // Navigation properties.
        // Parents.
        [JsonIgnore]
        public virtual Produce ?Produce { get; set; }
        [JsonIgnore]
        public virtual Supplier ?Supplier { get; set; }
    }

}
