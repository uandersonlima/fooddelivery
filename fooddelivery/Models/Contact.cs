using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Users;

namespace fooddelivery.Models
{
    public class Contact
    {
        [Key]
        public ulong Id { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Whatsapp { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }


        [Required, ForeignKey("User")]
        public ulong UserId { get; set; }
        public virtual User User { get; set; }
    }
}