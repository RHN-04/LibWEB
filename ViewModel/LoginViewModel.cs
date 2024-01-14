using System.ComponentModel.DataAnnotations;

namespace LibWEB.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите электронную почту")]
        [EmailAddress(ErrorMessage = "Неверный формат электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }
    }
}
