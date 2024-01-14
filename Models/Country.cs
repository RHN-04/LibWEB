using System;
using System.Collections.Generic;

namespace LibWEB.Models
{
    public partial class Country
    {
        public Country()
        {
            Authors = new HashSet<Author>();
        }

        public int Id { get; set; }
        public string CountryName { get; set; } = null!;

        public virtual ICollection<Author> Authors { get; set; }
    }
}
