using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fooddelivery.Models
{
    public class Address
    {
        [Key]
        public int Code { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string State { get; set; }
        public string Addon { get; set; }
        public bool Standard { get; set; }

        
        [Required, ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}