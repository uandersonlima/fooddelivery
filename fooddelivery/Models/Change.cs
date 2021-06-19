using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Contracts;

namespace fooddelivery.Models
{
    public class Change
    {
        [Key]
        public ulong Id { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }


        [ForeignKey("Suborder")]
        public ulong SuborderId { get; set; }
        public Suborder Suborder { get; set; }


        public ulong FoodId { get; set; }
        public ulong IngredientId { get; set; }
        
        [ForeignKey("FoodId,IngredientId")]
        public Additional Additional { get; set; }

    }
}