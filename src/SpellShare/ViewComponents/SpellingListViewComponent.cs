using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpellShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpellShare.ViewComponents
{
    public class SpellingListViewComponent : ViewComponent
    {
        private SpellShareDbContext _context;

        public SpellingListViewComponent(SpellShareDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var spellingLists = await _context.SpellingLists.Where(l => l.TeacherUsername == User.Identity.Name).ToListAsync();

            return View(spellingLists);
        }
    }
}
