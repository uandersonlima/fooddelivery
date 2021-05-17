using System.ComponentModel.DataAnnotations;
using fooddelivery.Libraries.Validations;

namespace fooddelivery.Models.DTO
{
    public class RegisterUserDTO
    {
        [MinLength(3)]
        [Required]
        public string Name { get; set; }

        [Required]
        [CPF]
        public string CPF { get; set; }

        [Required]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}