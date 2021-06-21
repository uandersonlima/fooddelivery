using System.ComponentModel.DataAnnotations;

namespace fooddelivery.Models.DTO
{
    public class UserResetPasswordDTO
    {
        public string ClientKey { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[$*&@#])(?:([0-9a-zA-Z$*&@#])(?!\\1)){8,}$", ErrorMessage = "Sua senha deve conter um dígito não alfanumérico, letras maiúsculas, minúsculas e números")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [Display(Name = "Confirme a senha")]
        [Compare("Password", ErrorMessage = "Senhas diferentes")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }

}