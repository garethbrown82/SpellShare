using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using SpellShare.Models;
using SpellShare.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellShare.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private SpellShareDbContext _context;

        public TeacherController(SpellShareDbContext context)
        {
            _context = context;
        }

        // GET: Teacher/AddStudent
        // Initial Add Student page.
        [HttpGet]
        public IActionResult AddStudent()
        {
            ViewData["message"] = "";

            return View();
        }

        // POST: Teacher/AddStudent
        // Add the new student to the database.
        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentViewModel newStudent)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student()
                {
                    Username = newStudent.Username,
                    FirstName = newStudent.FirstName,
                    LastName = newStudent.LastName,
                    Password = newStudent.Password,
                    TeacherUsername = User.Identity.Name
                };

                
                try
                {
                    _context.Students.Add(student);

                    await _context.SaveChangesAsync();

                    ViewData["message"] = $"You have successfully added {newStudent.FirstName} {newStudent.LastName} to the database.";
                }
                catch (Exception ex)
                {
                    ViewData["message"] = "There was an error updating the database. Please try again.";
                }
               
            }

            ModelState.Clear();

            return View();
        }

        // POST: Teacher/DeleteStudent
        // Delete the student.
        [HttpPost]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == id);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddStudent");
        }

        // GET: Teacher/EditStudent
        // Go to the Edit page for the chosen student.
        public async Task<IActionResult> EditStudent(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var student = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == id);

                StudentViewModel studentVM = new StudentViewModel()
                {
                    StudentId = student.StudentId,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Username = student.Username,
                    Password = student.Password
                };

                return View(studentVM);
            }
        }

        // POST: Teacher/EditStudent
        // Save edited changes made to the student in the database.
        [HttpPost]
        public async Task<IActionResult> EditStudent(StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                var studentToUpdate = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == student.StudentId);

                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.Username = student.Username;
                studentToUpdate.Password = student.Password;

                _context.Students.Update(studentToUpdate);

                await _context.SaveChangesAsync();
            }
            
            
            return RedirectToAction("AddStudent");
        }

        // GET: Teacher/CreateSpellingGroup
        // Create a new Spelling Group.
        public IActionResult CreateSpellingGroup()
        {
            return View();
        }

        // POST: Teacher/CreateSpellingGroup
        // Save the created Spelling Group to the database.
        [HttpPost]
        public async Task<IActionResult> CreateSpellingGroup(SpellingGroupViewModel SpellingGroupVM)
        {
            if(ModelState.IsValid)
            {
                SpellingGroup newList = new SpellingGroup()
                {
                    Name = SpellingGroupVM.Name,
                    Description = SpellingGroupVM.Description,
                    TeacherUsername = User.Identity.Name
                };

                try
                {
                    await _context.SpellingGroups.AddAsync(newList);

                    await _context.SaveChangesAsync();

                    ViewData["message"] = $"You have successfully created the Spelling Group named {newList.Name}";
                }
                catch (Exception ex) // Make a log to enter this Exception ex
                {
                    ViewData["message"] = "There was a problem updating the database. Please try again.";
                }       
            }

            return View();         
        }

        // POST: Teacher/DeleteGroup
        // Delete the chosen group from the database.
        [HttpPost]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var groupToDelete = await _context.SpellingGroups.SingleOrDefaultAsync(g => g.SpellingGroupId == id);

            _context.SpellingGroups.Remove(groupToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("CreateSpellingGroup");
        }

        // GET: Teacher/EditGroup
        // Go the the edit page for the selected group.
        public async Task<IActionResult> EditGroup(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else
            {
                var spellingGroup = await _context.SpellingGroups.SingleOrDefaultAsync(g => g.SpellingGroupId == id);

                SpellingGroupViewModel spellingGroupVM = new SpellingGroupViewModel()
                {
                    Name = spellingGroup.Name,
                    Description = spellingGroup.Description,
                    SpellingGroupId = spellingGroup.SpellingGroupId
                };

                return View(spellingGroupVM);
            }
        }

        // POST: Teacher/EditGroup
        // Save edited changes to the database
        [HttpPost]
        public async Task<IActionResult> EditGroup(SpellingGroupViewModel spellingGroupVM)
        {
            if(ModelState.IsValid)
            {
                var spellingGroup = await _context.SpellingGroups.SingleOrDefaultAsync(g => g.SpellingGroupId == spellingGroupVM.SpellingGroupId);

                spellingGroup.Name = spellingGroupVM.Name;
                spellingGroup.Description = spellingGroupVM.Description;

                _context.SpellingGroups.Update(spellingGroup);
                await _context.SaveChangesAsync();              
            }

            return RedirectToAction("CreateSpellingGroup");
        }

        // GET: Teacher/AssignStudents
        // Assign students to Spelling Groups
        public async Task<IActionResult> AssignStudents(int? SpellingGroupId)
        {
            AssignStudentsViewModel assignStudentsVM = new AssignStudentsViewModel();
            assignStudentsVM.SpellingGroups = await _context.SpellingGroups.Where(g => g.TeacherUsername == User.Identity.Name).ToListAsync();
            assignStudentsVM.AssignedStudents = new List<Student>();
            assignStudentsVM.UnassignedStudents = new List<Student>();

            // Assign first SpellingGroup in list to ActiveSpellingGroup.
            if (SpellingGroupId == null && assignStudentsVM.SpellingGroups.Count != 0)
            {
                SpellingGroupId = assignStudentsVM.SpellingGroups[0].SpellingGroupId;
            }

            if (SpellingGroupId != null)
            {
                // Get activated spelling group.
                assignStudentsVM.ActiveSpellingGroup = await _context.SpellingGroups.SingleOrDefaultAsync(g => g.SpellingGroupId == SpellingGroupId);

                ViewData["GroupMessage"] = "";

                // Get allocated students for active spelling group.
                var allocations = _context.StudentGroupAllocations.Where(a => a.SpellingGroupId == assignStudentsVM.ActiveSpellingGroup.SpellingGroupId).ToList();

                // Assign active group students to view model.
                foreach (var allocation in allocations)
                {
                    assignStudentsVM.AssignedStudents.Add(await _context.Students.SingleOrDefaultAsync(s => s.StudentId == allocation.StudentId));
                }

                // Assign unallocated students for active spelling group to Unassigned students list.
                var allStudents = _context.Students.Where(s => s.TeacherUsername == User.Identity.Name).ToList();

                foreach (var student in allStudents)
                {
                    bool allocated = false;
                    foreach (var allocation in allocations)
                    {
                        if (student.StudentId == allocation.StudentId)
                        {
                            allocated = true;
                        }
                    }
                    if (!allocated)
                    {
                        assignStudentsVM.UnassignedStudents.Add(student);
                    }
                }
            }
            else
            {
                ViewData["GroupMessage"] = "You have no groups.";
            }

            return View(assignStudentsVM);
        }

        // POST: Teacher/AddStudentToGroup
        // Add a student to a group, redirects to 'AssignStudents'.
        [HttpPost]
        public async Task<IActionResult> AddStudentToGroup(int Id, int GroupId)
        {
            Student student = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == Id);
            SpellingGroup group = await _context.SpellingGroups.SingleOrDefaultAsync(g => g.SpellingGroupId == GroupId);
            
            StudentGroupAllocation allocation = new StudentGroupAllocation()
            {
                Student = student,
                SpellingGroup = group
            };

            await _context.StudentGroupAllocations.AddAsync(allocation);
            await _context.SaveChangesAsync();
            

            return RedirectToAction("AssignStudents", new { SpellingGroupId = group.SpellingGroupId });
        }

        // POST: Teacher/RemoveFromGroup
        // Remove selected student from the group, redirects to 'AssignStudents'.
        [HttpPost]
        public async Task<IActionResult> RemoveFromGroup(int Id, int GroupId)
        {
            Student student = await _context.Students.SingleOrDefaultAsync(s => s.StudentId == Id);
            SpellingGroup group = await _context.SpellingGroups.SingleOrDefaultAsync(g => g.SpellingGroupId == GroupId);

            var allocation = await _context.StudentGroupAllocations.SingleOrDefaultAsync(a => a.Student == student && a.SpellingGroup == group);

            _context.StudentGroupAllocations.Remove(allocation);

            await _context.SaveChangesAsync();

            return RedirectToAction("AssignStudents", new { SpellingGroupId = group.SpellingGroupId });
        }

        // GET: Teacher/CreateSpellingList
        // Create new Spelling Lists page.
        public IActionResult CreateSpellingList()
        {
            return View();
        }

        // POST: Teacher/CreateSpellingList
        // Save the newly created spelling list to the database.
        [HttpPost]
        public async Task<IActionResult> CreateSpellingList(SpellingListViewModel spellingListVM)
        {
            if(ModelState.IsValid)
            {
                SpellingList newSpellingList = new SpellingList()
                {
                    Name = spellingListVM.Name,
                    Description = spellingListVM.Description,
                    TeacherUsername = User.Identity.Name
                };

                try
                {
                    await _context.SpellingLists.AddAsync(newSpellingList);

                    await _context.SaveChangesAsync();

                    ViewData["message"] = $"You have successfully added the Spelling List named {newSpellingList.Name}";
                }
                catch (Exception ex)
                {
                    ViewData["message"] = "There was an error updating the database. Please try again.";
                }

                
            }
            ModelState.Clear();

            return View();
        }

        // GET: Teacher/EditList
        // Edit the chosen list
        public async Task<IActionResult> EditList(int? Id)
        {
            if (Id == null)
            {
                return NotFound();   
            }
            else
            {
                var spellingList = await _context.SpellingLists.SingleOrDefaultAsync(l => l.SpellingListId == Id);

                SpellingListViewModel spellingListVM = new SpellingListViewModel()
                {
                    SpellingListId = spellingList.SpellingListId,
                    Name = spellingList.Name,
                    Description = spellingList.Description
                };

                return View(spellingListVM);
            }
        }

        // POST: Teacher/EditList
        // Save the edited changes for Spelling List to the database.
        [HttpPost]
        public async Task<IActionResult> EditList(SpellingListViewModel spellingListVM)
        {
            if(ModelState.IsValid)
            {
                var spellingList = await _context.SpellingLists.SingleOrDefaultAsync(l => l.SpellingListId == spellingListVM.SpellingListId);

                spellingList.Name = spellingListVM.Name;
                spellingList.Description = spellingListVM.Description;

                _context.SpellingLists.Update(spellingList);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("CreateSpellingList");
        }

        // POST: Teacher/DeleteList
        // Delete the chosen Spelling List
        [HttpPost]
        public async Task<IActionResult> DeleteList(int Id)
        {
            var listToDelete = await _context.SpellingLists.SingleOrDefaultAsync(l => l.SpellingListId == Id);

            _context.SpellingLists.Remove(listToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("CreateSpellingList");
        }

        // GET: Teacher/AssignWords
        // Create and Assign words to Spelling Lists.
        public async Task<IActionResult> AssignWords(int? SpellingListId)
        {
            AssignWordsViewModel assignWordsVM = new AssignWordsViewModel()
            {
                SpellingLists = await _context.SpellingLists.Where(l => l.TeacherUsername == User.Identity.Name).ToListAsync()
            };

            
            if(SpellingListId == null && assignWordsVM.SpellingLists.Count != 0)
            {
                assignWordsVM.ActiveSpellingList = assignWordsVM.SpellingLists[0];
                assignWordsVM.SpellingListName = assignWordsVM.ActiveSpellingList.Name;
            }

            if (SpellingListId != null)
            {
                assignWordsVM.ActiveSpellingList = await _context.SpellingLists.SingleOrDefaultAsync(l => l.SpellingListId == SpellingListId);

                assignWordsVM.SpellingListName = assignWordsVM.ActiveSpellingList.Name;
            }

            assignWordsVM.SpellingListId = assignWordsVM.ActiveSpellingList.SpellingListId;

            // Get all Word List Allocations for Active Spelling List.
            assignWordsVM.WordAllocations = await _context.WordListAllocations.Where(a => a.SpellingListId == assignWordsVM.ActiveSpellingList.SpellingListId).ToListAsync();

            foreach(WordListAllocation allocation in assignWordsVM.WordAllocations)
            {
                allocation.SpellingWord = await _context.SpellingWords.SingleOrDefaultAsync(w => w.Id == allocation.SpellingWordId);
            }

            return View(assignWordsVM);
        }

        // POST: Teacher/AddWordToList
        // Creates and adds word to Spelling List
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddWordToList(string SpellingWord, int SpellingListId)
        {
            SpellingWord newWord;

            SpellingWord existingWord = await _context.SpellingWords.SingleOrDefaultAsync(w => w.Word == SpellingWord);

            // Creates a new word if it does not already exist, otherwise retreive existing word to add to list.
            if(existingWord == null)
            {
                newWord = new SpellingWord
                {
                    Word = SpellingWord
                };

                await _context.SpellingWords.AddAsync(newWord);
            }
            else
            {
                newWord = existingWord;
            }

            WordListAllocation newAllocation = new WordListAllocation()
            {
                SpellingWordId = newWord.Id,
                SpellingListId = SpellingListId
            };

            await _context.WordListAllocations.AddAsync(newAllocation);

            await _context.SaveChangesAsync();

            return RedirectToAction("AssignWords", (new { SpellingListId = SpellingListId }));
        }

        // POST: Teacher/DeleteWordAllocation
        // Remove the selected word from the Spelling List
        [HttpPost]
        public async Task<IActionResult> DeleteWordAllocation(int Id, int ActiveListId)
        {
            WordListAllocation allocation = await _context.WordListAllocations.SingleOrDefaultAsync(a => a.WordListAllocationId == Id);

            _context.WordListAllocations.Remove(allocation);

            await _context.SaveChangesAsync();

            return RedirectToAction("AssignWords", (new { SpellingListId = ActiveListId }));
        }

        // GET: Teacher/AssignLists
        // Assign Spelling Lists to Spelling Groups.
        public async Task<IActionResult> AssignLists(int? SpellingGroupId)
        {
            AssignListsViewModel assignListsVM = new AssignListsViewModel()
            {
                SpellingGroups = await _context.SpellingGroups.Where(g => g.TeacherUsername == User.Identity.Name).ToListAsync()
            };

            assignListsVM.UnassignedSpellingLists = new List<SpellingList>();
            assignListsVM.AssignedSpellingLists = new List<SpellingList>();

            if (SpellingGroupId == null)
            {
                assignListsVM.ActiveSpellingGroup = await _context.SpellingGroups.FirstAsync();
            }
            else
            {
                assignListsVM.ActiveSpellingGroup = await _context.SpellingGroups.SingleOrDefaultAsync(g => g.SpellingGroupId == SpellingGroupId);
            }

            List<ListGroupAllocation> allocations = await _context.ListGroupAllocations.Where(a => a.SpellingGroupId == assignListsVM.ActiveSpellingGroup.SpellingGroupId).ToListAsync();

            List<SpellingList> allSpellingLists = await _context.SpellingLists.Where(l => l.TeacherUsername == User.Identity.Name).ToListAsync();

            foreach(var list in allSpellingLists)
            {
                bool isAssignedToGroup = false;

                foreach(var a in allocations)
                {
                    if(list.SpellingListId == a.SpellingListId)
                    {
                        isAssignedToGroup = true;
                    }
                }

                if(isAssignedToGroup)
                {
                    assignListsVM.AssignedSpellingLists.Add(list);
                }
                else
                {
                    assignListsVM.UnassignedSpellingLists.Add(list);
                }
            }

            return View(assignListsVM);
        }

        // POST: Teacher/AddListToGroup
        // Add the Spelling List to a Spelling Group in the database.
        [HttpPost]
        public async Task<IActionResult> AddListToGroup(int Id, int SpellingGroupId)
        {
            ListGroupAllocation allocation = new ListGroupAllocation()
            {
                SpellingListId = Id,
                SpellingGroupId = SpellingGroupId
            };

            await _context.ListGroupAllocations.AddAsync(allocation);

            await _context.SaveChangesAsync();

            return RedirectToAction("AssignLists", new { SpellingGroupId = SpellingGroupId });
        }

        // POST: Teacher/RemoveListFromGroup
        // Remove the Spelling List from the selected Spelling Group.
        [HttpPost]
        public async Task<IActionResult> RemoveListFromGroup(int Id, int SpellingGroupId)
        {
            ListGroupAllocation allocation = await _context.ListGroupAllocations.SingleOrDefaultAsync(a => a.SpellingListId == Id && a.SpellingGroupId == SpellingGroupId);

            _context.ListGroupAllocations.Remove(allocation);

            await _context.SaveChangesAsync();

            return RedirectToAction("AssignLists", new { SpellingGroupId = SpellingGroupId });
        }
    }
}
