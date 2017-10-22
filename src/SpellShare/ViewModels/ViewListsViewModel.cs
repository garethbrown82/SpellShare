using SpellShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.ViewModels
{
    public class ViewListsViewModel
    {
        public Student Student { get; set; }
        public List<SpellingList> SpellingLists { get; set; }
        public int SpellingListId { get; set; }
        public SpellingList ActiveSpellingList { get; set; }
        public List<SpellingWord> SpellingWords { get; set; }
    }
}
