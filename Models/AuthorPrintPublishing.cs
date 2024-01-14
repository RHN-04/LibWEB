using System;
using System.Collections.Generic;

namespace LibWEB.Models
{
    public partial class AuthorPrintPublishing
    {
        public int? Author { get; set; }
        public int PrintPublishing { get; set; }

        public class AuthorPrintPublishingKey
        {
            public int? Author { get; set; }
            public int PrintPublishing { get; set; }
        }

        public virtual Author? AuthorNavigation { get; set; }
        public virtual PrintPublishing PrintPublishingNavigation { get; set; } = null!;
    }
}
