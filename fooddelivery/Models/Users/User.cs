using System.Collections.Generic;
using fooddelivery.Libraries.Validations;
using fooddelivery.Models.Contracts;
using Microsoft.AspNetCore.Identity;

namespace fooddelivery.Models.Users
{
    public class User : IdentityUser<ulong>
    {
        public string Name { get; set; }
        
        public string CPF {get; set;}

        public List<Contact> Contacts { get; set; }
        public List<Feedbacks> Feedbacks { get; set; }
        public List<Address> Addresses { get; set; }
    }
}