using System;
using System.Collections.Generic;

namespace LibWEB.Models
{
    public partial class Preorder
    {
        public int Id { get; set; }
        public DateTime? GivingDeadlineDate { get; set; }
        public int Reader { get; set; }
        public string Status { get; set; }
        public int PrintPublishing { get; set; }

        public virtual Reader ReaderNavigation { get; set; }
        public virtual PrintPublishing PrintPublishingNavigation { get; set; }
    }
}
