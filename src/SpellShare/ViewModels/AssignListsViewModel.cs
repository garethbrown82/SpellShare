using SpellShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.ViewModels
{
    public class AssignListsViewModel
    {
        public List<SpellingGroup> SpellingGroups { get; set; }
        public SpellingGroup ActiveSpellingGroup { get; set; }
        public int SpellingGroupId { get; set; }
        public List<SpellingList> AssignedSpellingLists { get; set; }
        public List<SpellingList> UnassignedSpellingLists { get; set; }
    }
}
