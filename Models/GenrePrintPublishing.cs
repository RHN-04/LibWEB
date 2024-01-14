using System;
using System.Collections.Generic;

namespace LibWEB.Models
{
    public partial class GenrePrintPublishing
    {
        public int Genre { get; set; }
        public int PrintPublishing { get; set; }
        public class GenrePrintPublishingKey
        {
            public int Genre { get; set; }
            public int PrintPublishing { get; set; }
        }
        public virtual Genre GenreNavigation { get; set; } = null!;
        public virtual PrintPublishing PrintPublishingNavigation { get; set; } = null!;
    }
}
