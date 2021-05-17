using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Libraries.Validations;
using fooddelivery.Models.Contracts;
using Microsoft.AspNetCore.Identity;

namespace fooddelivery.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string CPF {get; set;}

        public List<Contact> Contacts { get; set; }
        public List<Feedbacks> Feedbacks { get; set; }
        public List<Address> Addresses { get; set; }
    }
}