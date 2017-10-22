using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpellShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellShare.ViewComponents
{
    public class StudentListViewComponent : ViewComponent
    {
        private SpellShareDbContext _context;

        public StudentListViewComponent(SpellShareDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var students = await _context.Students.Where(s => s.TeacherUsername == User.Identity.Name).ToListAsync();

            return View(students);
        }
    }
}
