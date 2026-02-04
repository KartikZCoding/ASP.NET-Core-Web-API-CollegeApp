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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Student>().HasData(new List<Student>()
            {
                new Student
                { Id = 1,
                    StudentName = "Kartik",
                    Email = "Kartik123@gmail.com",
                    Address = "Hyd, India",
                    DOB = new DateTime(2005,08,03)
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Aryan",
                    Email = "Aryan123@gmail.com",
                    Address = "Banglore, India",
                    DOB = new DateTime(2004,09,03)
                }
            });*/

            //table 1
            modelBuilder.ApplyConfiguration(new StudentConfig());

            //table 2...
        }
    }
}
