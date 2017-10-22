using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.ViewModels
{
    public class StudentLoginViewModel
    {
        [Required]
        public string TeacherUserName { get; set; }

        [Required]
        public string StudentUserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string StudentPassword { get; set; }
    }
}
