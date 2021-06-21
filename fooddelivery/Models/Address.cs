using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Users;

namespace fooddelivery.Models
{
    public class Address
    {
        [Key]
        public ulong Id { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string State { get; set; }
        public string Addon { get; set; }
        public bool Standard { get; set; }

        public string AddressType { get; set; }


        public double? X_coordinate {get; set;}
        public double? Y_coordinate {get; set;}

        
       
        [Required, ForeignKey("User")]
        public ulong UserId { get; set; }
        public virtual User User { get; set; }
    }
}