using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellShare.Models
{
    public class SpellShareDbContext : IdentityDbContext<SpellShareUser>
    {
        public SpellShareDbContext(DbContextOptions<SpellShareDbContext> options) : base(options)
        {
        }

        public DbSet<SpellingWord> SpellingWords { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SpellingGroup> SpellingGroups { get; set; }
        public DbSet<StudentGroupAllocation> StudentGroupAllocations { get; set; }
        public DbSet<SpellingList> SpellingLists { get; set; }
        public DbSet<WordListAllocation> WordListAllocations { get; set; }
        public DbSet<ListGroupAllocation> ListGroupAllocations { get; set; }

    }
}