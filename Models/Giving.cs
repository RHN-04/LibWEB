using System;
using System.Collections.Generic;

namespace LibWEB.Models
{
    public partial class Giving
    {
        public int Id { get; set; }
        public DateTime? GivingDate { get; set; }
        public int Reader { get; set; }
        public DateTime? ReceivingDate { get; set; }
        public DateTime? ReceivingDeadlineDate { get; set; }
        public int PrintPublishing { get; set; }
        public virtual Reader ReaderNavigation { get; set; }
        public virtual PrintPublishing PrintPublishingNavigation { get; set; } 
    }
}
