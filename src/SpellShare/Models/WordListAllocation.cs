using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.Models
{
    public class WordListAllocation
    {
        public int WordListAllocationId { get; set; }

        public int SpellingWordId { get; set; }

        public SpellingWord SpellingWord { get; set; }

        public int SpellingListId { get; set; }

        public SpellingList SpellingList { get; set; }
    }
}
