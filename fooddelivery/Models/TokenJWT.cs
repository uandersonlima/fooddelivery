using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Users;

namespace fooddelivery.Models
{
    public class TokenJWT
    {
        [Key]
        public ulong Id { get; set; }
        public string RefreshToken { get; set; }
        [ForeignKey("User")]
        public ulong UserId { get; set; }
        public virtual User User { get; set; }
        public bool Used { get; set; }
        public DateTime ExpirationToken { get; set; }
        public DateTime ExpirationRefreshToken { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}