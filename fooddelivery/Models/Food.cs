using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Contracts;

namespace fooddelivery.Models
{
    public class Food
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        
        [NotMapped]
        public bool Available { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }   
         

        public List<Suborder> Suborders { get; set; }
        public List<Image> Images { get; set; }
	    public List<FoodIngredients> FoodIngredients{ get; set; }
    }
}