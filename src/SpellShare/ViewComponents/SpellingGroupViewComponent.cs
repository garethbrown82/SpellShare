using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpellShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.ViewComponents
{
    public class SpellingGroupViewComponent : ViewComponent
    {
        private SpellShareDbContext _context;

        public SpellingGroupViewComponent(SpellShareDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var groups = await _context.SpellingGroups.Where(g => g.TeacherUsername == User.Identity.Name).ToListAsync();

            return View(groups);
        }
    }
}
