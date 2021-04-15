using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fooddelivery.Models
{
    public class Food
    {
        [Key]
        public int Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }


        public int CategoryCode { get; set; }
        public Category Category { get; set; }    

        public List<Suborder> Suborders { get; set; }
        public List<Image> Images { get; set; }
	    public List<Ingredient> Ingredients{ get; set; }
    }
}