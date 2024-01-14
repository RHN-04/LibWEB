using System.ComponentModel.DataAnnotations;

namespace LibWEB.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введите фамилию.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите имя.")]
        public string Name { get; set; }

        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Выберите дату рождения.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Введите адрес электронной почты.")]
        [EmailAddress(ErrorMessage = "Неверный формат адреса электронной почты.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

}
