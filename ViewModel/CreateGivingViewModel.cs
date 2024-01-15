using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibWEB.ViewModel
{
    public class CreateGivingViewModel
    {
        [Display(Name = "Книга")]
        public int BookId { get; set; }
        public string BookName { get; set; }

        [Required(ErrorMessage = "Выберите читателя")]
        [Display(Name = "Читатель")]
        public int ReaderId { get; set; }

        [Required(ErrorMessage = "Укажите дату выдачи")]
        [Display(Name = "Дата выдачи")]
        [DataType(DataType.Date)]
        public DateTime? GivingDate { get; set; }

        [Required(ErrorMessage = "Укажите срок сдачи")]
        [Display(Name = "Срок сдачи")]
        [DataType(DataType.Date)]
        public DateTime? ReceivingDeadlineDate { get; set; }
        public SelectList? Readers { get; set; }
    }

    public class UpdateGivingViewModel : CreateGivingViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Пожалуйста, введите корректную дату")]
        public DateTime? ReceivingDate { get; set; }
        [Display(Name = "ФИО читателя")]
        public string? ReaderFullName { get; set; }
    }

}
