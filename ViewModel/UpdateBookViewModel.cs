using LibWEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibWEB.ViewModel
{
    public class UpdateBookViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введите название книги")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Выберите возрастное ограничение")]
        public int AgeRestriction { get; set; }
        public List<SelectListItem> AgeRestrictionOptions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "0", Text = "0+" },
            new SelectListItem { Value = "6", Text = "6+" },
            new SelectListItem { Value = "12", Text = "12+" },
            new SelectListItem { Value = "16", Text = "16+" },
            new SelectListItem { Value = "18", Text = "18+" }
        };

        public class YearRangeAttribute : RangeAttribute
        {
            public YearRangeAttribute(int minimum) : base(minimum, DateTime.Now.Year)
            {
                ErrorMessage = $"Год издания должен быть в пределах от {minimum} до {DateTime.Now.Year}";
            }
        }

        [YearRange(0, ErrorMessage = "Введите корректный год издания")]
        [Required(ErrorMessage = "Введите год издания")]
        public int YearOfPublishing { get; set; }

        [Required(ErrorMessage = "Введите краткое описание")]
        public string Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Количество должно быть положительным числом")]
        [Required(ErrorMessage = "Введите количество книг")]
        public int Numbers { get; set; }

        public string? ImageId { get; set; }

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        public List<Author>? Authors { get; set; }
        public List<Genre>? Genres { get; set; }

        [Display(Name = "Авторы")]
        public List<int>? SelectedAuthorIds { get; set; }

        [Required(ErrorMessage = "Выберите жанры")]
        [Display(Name = "Жанры")]
        public List<int> SelectedGenreIds { get; set; }
    }
}
