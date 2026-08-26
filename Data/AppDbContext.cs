using Microsoft.EntityFrameworkCore;
using StudentManagementSystemMVC.Models;
using StudentManagementSystemMVC.Config;

namespace StudentManagementSystemMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder)  
        {}
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentConfiguration).Assembly);
        }
    }
}
