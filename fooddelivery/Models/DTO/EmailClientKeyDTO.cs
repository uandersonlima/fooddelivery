using System.ComponentModel.DataAnnotations;

namespace fooddelivery.Models.DTO
{
    public class EmailClientKeyDTO
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        public string ClientKey { get; set; }
    }
}