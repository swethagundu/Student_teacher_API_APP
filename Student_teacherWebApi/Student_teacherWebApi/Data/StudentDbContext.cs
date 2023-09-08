using Microsoft.EntityFrameworkCore;
using Student_teacherWebApi.Models;

namespace Student_teacherWebApi.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> STUDENT { get; set; }
        public DbSet<Teacher> TEACHER { get; set; }
        public DbSet<Course> COURSE { get; set; }
    }
}
