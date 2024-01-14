using System;
using System.Collections.Generic;

namespace LibWEB.Models
{
    public partial class PrintPublishing
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int AgeRestriction { get; set; }
        public int YearOfPublishing { get; set; }
        public string Description { get; set; } = null!;
        public int Numbers { get; set; }
        public string? ImageId { get; set; }
    }
}
