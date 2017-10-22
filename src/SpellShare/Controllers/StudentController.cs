using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpellShare.ViewModels;
using SpellShare.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SpellShare.Controllers
{
    public class StudentController : Controller
    {
        private SpellShareDbContext _context;

        public StudentController(SpellShareDbContext context)
        {
            _context = context;
        }
        // GET: /<controller>/

        public async Task<IActionResult> LoginStudent(StudentLoginViewModel studentLoginVM)
        {
            Student student = await _context.Students.SingleOrDefaultAsync(s => s.TeacherUsername == studentLoginVM.TeacherUserName && s.Username == studentLoginVM.StudentUserName && s.Password == studentLoginVM.StudentPassword);

            //ViewListsViewModel viewModel = new ViewListsViewModel()
            //{
            //    Student = student
            //};

            return RedirectToAction("ViewLists", new { StudentId = student.StudentId });
        }

        // GET: Student/ViewLists
        public async Task<IActionResult> ViewLists(int StudentId, int? SpellingListId)
        {
            ViewListsViewModel viewListsVM = new ViewListsViewModel();

            viewListsVM.Student = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == StudentId);

            // Get all group allocations for the student and add the group to a list
            List<StudentGroupAllocation> groupAllocations = await _context.StudentGroupAllocations.Where(a => a.StudentId == viewListsVM.Student.StudentId).ToListAsync();
            List<SpellingGroup> studentGroups = new List<SpellingGroup>();

            foreach(var allocation in groupAllocations)
            {
                SpellingGroup group = await _context.SpellingGroups.SingleOrDefaultAsync(g => g.SpellingGroupId == allocation.SpellingGroupId);

                studentGroups.Add(group);
            }

            viewListsVM.SpellingLists = new List<SpellingList>();

            // Assign all spelling lists for each group the student is in to the View Model List of Spelling Lists
            foreach (var group in studentGroups)
            {
                List<ListGroupAllocation> listAllocations = await _context.ListGroupAllocations.Where(a => a.SpellingGroupId == group.SpellingGroupId).ToListAsync();

                foreach(var a in listAllocations)
                {
                    SpellingList list = await _context.SpellingLists.SingleOrDefaultAsync(l => l.SpellingListId == a.SpellingListId);

                    viewListsVM.SpellingLists.Add(list);
                }
            }

            // Assing a spelling list to the active list
            if(SpellingListId == null)
            {
                viewListsVM.ActiveSpellingList = viewListsVM.SpellingLists.First();
            }
            else
            {
                viewListsVM.ActiveSpellingList = viewListsVM.SpellingLists.SingleOrDefault(l => l.SpellingListId == SpellingListId);
            }

            // Get all the words associated with the Spelling list
            List<WordListAllocation> wordAllocations = await _context.WordListAllocations.Where(a => a.SpellingListId == viewListsVM.ActiveSpellingList.SpellingListId).ToListAsync();

            viewListsVM.SpellingWords = new List<SpellingWord>();

            foreach(var a in wordAllocations)
            {
                SpellingWord word = await _context.SpellingWords.SingleOrDefaultAsync(w => w.Id == a.SpellingWordId);

                viewListsVM.SpellingWords.Add(word);
            }

            return View(viewListsVM);
        }
    }
}
