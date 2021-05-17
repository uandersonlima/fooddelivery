using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fooddelivery.Models.Contracts
{
    public class FoodIngredients
    {
        
        [ForeignKey("Food"),Column(Order = 0)]
        public long FoodId { get; set; }
        public Food Food { get; set; }


        [ForeignKey("Ingredient"), Column(Order = 1)]
        public long IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

    }
}