using LibWEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibWEB.ViewModel
{
    public static class ReaderExtensions
    {
        public static string GetFullName(this Reader reader)
        {
            if (reader == null)
                return string.Empty;

            if (string.IsNullOrWhiteSpace(reader.Patronymic))
            {
                return $"{reader.Surname} {reader.Name}";
            }
            else
            {
                return $"{reader.Surname} {reader.Name} {reader.Patronymic}";
            }
        }
    }

    public class GivingViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }

        [Display(Name = "Название книги")]
        public string BookTitle { get; set; }

        [Display(Name = "Читатель")]
        public int ReaderId { get; set; }

        public SelectList Readers { get; set; }

        [Display(Name = "Срок сдачи")]
        public DateTime? ReceivingDeadlineDate { get; set; }

        [Display(Name = "Дата сдачи книги")]
        public DateTime? ReceivingDate { get; set; }
        [Display(Name = "Полное имя читателя")]
        public string ReaderFullName { get; set; }
    }
}
