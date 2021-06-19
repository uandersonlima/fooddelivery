using System;
using System.ComponentModel.DataAnnotations;

namespace fooddelivery.Models.Access
{
    public class AccessKey
    {
        public string Key { get; set; }
        public string Email { get; set; }
        public string KeyType { get; set; }
        public DateTime DataGerada { get; set; }
    }
}
