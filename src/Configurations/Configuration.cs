using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RangeExtensions.Configurations
{
    public class AddStrategy
    {
        public bool ExpandToNextWhenNull { get; set; }
        public bool S { get; set; }
        public bool AllowOverlap { get; set; }
        public bool AllowGaps { get; set; } = true;
        public bool UpdateOnExactMatch { get; set; }
        public bool ShrinkPreviousTo { get; set; }
    }
}
