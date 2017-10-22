using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.Models
{
    public class ListGroupAllocation
    {
        public int ListGroupAllocationId { get; set; }
        public int SpellingListId { get; set; }
        public SpellingList SpellingList { get; set; }
        public int SpellingGroupId { get; set; }
        public SpellingGroup SpellingGroup { get; set; }
    }
}
