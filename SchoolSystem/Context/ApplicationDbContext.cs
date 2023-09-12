using Microsoft.EntityFrameworkCore;
using SchoolSystem.Models;
using SchoolSystem.ViewModels;

namespace SchoolSystem.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentSubject>()
                .HasKey(e => new { e.StudentId, e.SubjectId });
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        
    }
}
