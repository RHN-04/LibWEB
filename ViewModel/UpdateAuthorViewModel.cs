using LibWEB.Models;
using System.ComponentModel.DataAnnotations;

namespace LibWEB.ViewModel
{
    public class UpdateAuthorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя автора")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите страну рождения автора")]
        [Display(Name = "Страна")]
        public int Country { get; set; }

        [Required(ErrorMessage = "Введите фамилию автора")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }
        public List<Country>? Countries { get; set; }

        [Display(Name = "Новая страна")]
        public string? NewCountry { get; set; }
    }

}
