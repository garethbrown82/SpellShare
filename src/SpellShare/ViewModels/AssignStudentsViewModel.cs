using SpellShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.ViewModels
{
    public class AssignStudentsViewModel
    {
        public int SpellingGroupId { get; set; }

        public List<SpellingGroup> SpellingGroups { get; set; }

        public SpellingGroup ActiveSpellingGroup { get; set; }

        public List<Student> UnassignedStudents { get; set; }

        public List<Student> AssignedStudents { get; set; }
    }
}
