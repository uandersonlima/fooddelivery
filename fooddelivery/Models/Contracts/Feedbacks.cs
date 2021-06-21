using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Users;

namespace fooddelivery.Models.Contracts
{
    public class Feedbacks
    {
        [ForeignKey("User"), Column(Order=0)]
        public ulong UserId { get; set; }
        public virtual User User { get; set; }


        [ForeignKey("Order"), Column(Order=1)]
        public ulong OrderId { get; set; }
        public Order Order { get; set; }

        public int Score { get; set; }
        public string Note { get; set; }
    }
}