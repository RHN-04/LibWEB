using System.ComponentModel.DataAnnotations;

namespace LibWEB.ViewModel
{
    public class AuthorViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Имя")]
        public string Name { get; set; }
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }
        [Display(Name = "Отчество")]
        public string? Patronymic { get; set; }
        [Display(Name = "Страна")]
        public string CountryName { get; set; }

    }
}
