using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Day02Exercises.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }

        // Navigation properties.
        // Child.
        [JsonIgnore]
        public virtual ICollection<ProduceSupplier>
        ?ProduceSuppliers { get; set; }
    }

}
