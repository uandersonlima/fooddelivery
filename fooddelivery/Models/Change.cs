using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Contracts;

namespace fooddelivery.Models
{
    public class Change
    {
        [Key]
        public int Code { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }


        [ForeignKey("Suborder")]
        public int SuborderCode { get; set; }
        public Suborder Suborder { get; set; }


        public int FoodCode { get; set; }
        public int IngredientCode { get; set; }
        
        [ForeignKey("FoodCode,IngredientCode")]
        public Additional Additional { get; set; }

    }
}