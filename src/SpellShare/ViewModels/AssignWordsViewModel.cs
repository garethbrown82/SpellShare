using SpellShare.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.ViewModels
{
    public class AssignWordsViewModel
    {
        public List<SpellingList> SpellingLists { get; set; }

        public SpellingList ActiveSpellingList { get; set; }

        public string SpellingListName { get; set; }

        public int SpellingListId { get; set; }

        public List<WordListAllocation> WordAllocations { get; set; }

        [Required]
        public string SpellingWord { get; set; }
    }
}
