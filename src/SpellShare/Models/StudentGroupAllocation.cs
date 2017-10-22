using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.Models
{
    public class StudentGroupAllocation
    {
        public int StudentGroupAllocationId { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int SpellingGroupId { get; set; }
        public SpellingGroup SpellingGroup { get; set; }
    }
}
