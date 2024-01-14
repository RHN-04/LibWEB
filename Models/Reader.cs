using System;
using System.Collections.Generic;

namespace LibWEB.Models
{
    public partial class Reader
    {
        public Reader()
        {
            Givings = new HashSet<Giving>();
            Preorders = new HashSet<Preorder>();
        }

        public int Id { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string EmailAddress { get; set; } = null!;

        public virtual ICollection<Giving> Givings { get; set; }
        public virtual ICollection<Preorder> Preorders { get; set; }
    }
}
