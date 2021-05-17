using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Contracts;

namespace fooddelivery.Models
{
    public class Change
    {
        [Key]
        public long Id { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }


        [ForeignKey("Suborder")]
        public long SuborderId { get; set; }
        public Suborder Suborder { get; set; }


        public long FoodId { get; set; }
        public long IngredientId { get; set; }
        
        [ForeignKey("FoodId,IngredientId")]
        public Additional Additional { get; set; }

    }
}