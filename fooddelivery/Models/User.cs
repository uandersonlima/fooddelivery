using System.Collections.Generic;
using fooddelivery.Models.Contracts;
using Microsoft.AspNetCore.Identity;

namespace fooddelivery.Models
{
    public class User : IdentityUser
    {
        public List<Contact> Contacts { get; set; }
        public List<Feedbacks> Feedbacks { get; set; }
        public List<Address> Addresses { get; set; }
    }
}