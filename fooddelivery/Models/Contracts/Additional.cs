using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace fooddelivery.Models.Contracts
{
    public class Additional
    {

        [ForeignKey("Food"), Column(Order=0)]
        public ulong FoodId { get; set; }
        public Food Food { get; set; }


        [ForeignKey("Ingredient"), Column(Order=1)]
        public ulong IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }


        public List<Change> Changes { get; set; }
    }
}