using System.ComponentModel.DataAnnotations;

namespace fooddelivery.Models.DTO
{
    public class EmailDTO
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}