using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Day02Exercises.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }

        // Navigation properties.
        // Child.
        [JsonIgnore]
        public ICollection<ProduceSupplier>?
        ProduceSuppliers { get; set; }
    }

}
