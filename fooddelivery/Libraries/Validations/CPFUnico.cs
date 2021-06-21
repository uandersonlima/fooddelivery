using System.ComponentModel.DataAnnotations;
using System.Linq;
using fooddelivery.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace fooddelivery.Libraries.Validations
{
    public sealed class  CPFUnico : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Digite o CPF");
            }

            string CPF = (value as string).Trim();

            var _userManager = (UserManager<User>)validationContext.GetService(typeof(UserManager<User>));
            User user = _userManager.Users.Where(user => user.CPF == CPF).FirstOrDefault();

            if (user != null)
            {
                return new ValidationResult("CPF já cadastrado, entre em contato em caso de dúvidas");
            }

            return ValidationResult.Success;
        }
    }
}