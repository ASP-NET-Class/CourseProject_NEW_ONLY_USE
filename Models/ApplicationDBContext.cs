using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Swimmer> Swimmers { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public ApplicationDbContext(DbContextOptions
            <ApplicationDbContext> options) : base(options)
        { }
    }
}
