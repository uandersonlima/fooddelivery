using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Contracts;

namespace fooddelivery.Models
{
    public class Ingredient
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }


        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string Unity { get; set; }

        public List<FoodIngredients> FoodIngredients{ get; set; }
    }
}