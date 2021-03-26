using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fooddelivery.Models
{
    public class Ingredient
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string Unity { get; set; }
    }
}