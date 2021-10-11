using System.ComponentModel.DataAnnotations;

namespace fooddelivery.Models.DTO
{
    public class ChangePasswordDTO
    {
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres")]
        [RegularExpression("^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[$*&@#])(?:([0-9a-zA-Z$*&@#])(?!\\1)){8,}$", ErrorMessage = "Sua senha deve conter um dígito não alfanumérico, letras maiúsculas, minúsculas e números")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Informe o campo {0}", AllowEmptyStrings = false)]
        [Display(Name = "Confirme a senha")]
        [Compare("NewPassword", ErrorMessage = "Senhas diferentes")]
        [DataType(DataType.Password)]
        public string NewPasswordConfirmation { get; set; }
    }
}