using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fooddelivery.Models
{
    public class Suborder
    {
        [Key]
        public int Code { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public string Note { get; set; }


        [ForeignKey("Food")]
        public int FoodCode { get; set; }
        public Food Food { get; set; }

        [ForeignKey("Order")]
        public int OrderCode { get; set; }
        public Order Order { get; set; }

        public List<Change> Changes { get; set; }

    }
}