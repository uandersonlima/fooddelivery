using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Enums;

namespace fooddelivery.Models
{
    public class Order
    {
        [Key]
        public int Code { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public string Note { get; set; }

        public DeliveryStatus DeliveryStatus { get; set; }



        [ForeignKey("Address")]
        public int AddressCode { get; set; }
        public Address Address { get; set; }

        public List<Suborder> Suborders { get; set; }
        public List<Feedbacks> Feedbacks { get; set; }
    }
}