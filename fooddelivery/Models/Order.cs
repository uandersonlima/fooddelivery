using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Contracts;
using fooddelivery.Models.Users;

namespace fooddelivery.Models
{
    public class Order
    {
        [Key]
        public ulong Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }

        public string Note { get; set; }

        public DateTime ShoppingTime { get; set; }


        [ForeignKey("DeliveryStatus")]
        public ulong DeliveryStatusId { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }


        [ForeignKey("PaymentType")]
        public ulong PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }



        [ForeignKey("Address")]
        public ulong AddressId { get; set; }
        public Address Address { get; set; }


        [Required, ForeignKey("User")]
        public ulong UserId { get; set; }
        public virtual User User { get; set; }


        public List<Suborder> Suborders { get; set; }
        public List<Feedbacks> Feedbacks { get; set; }
    }
}