using ASPNETCoreWebAPI.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPI.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //table 1
            modelBuilder.ApplyConfiguration(new StudentConfig());
           
            //table 2
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
        }
    }
}
