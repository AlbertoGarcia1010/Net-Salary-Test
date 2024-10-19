using Microsoft.EntityFrameworkCore;
using Salary.Models.Entities;

namespace Salary.Models.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext()
        {
        }

        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Associate> Associates { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Increment> Increments { get; set; }

    }
}
