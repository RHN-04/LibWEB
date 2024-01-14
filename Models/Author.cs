using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibWEB.Models
{
    public partial class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Country { get; set; }
        public string Surname { get; set; } = null!;
        public string? Patronymic { get; set; }
        public virtual Country CountryNavigation { get; set; } = null!;
    }
}
