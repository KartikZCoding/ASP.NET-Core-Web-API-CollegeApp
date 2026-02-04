using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace ASPNETCoreWebAPI.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(t => t.Id); // primary key

            builder.Property(x => x.Id).UseIdentityColumn(); // auto generated

            builder.Property(n => n.StudentName).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            builder.HasData(new List<Student>()
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
            });
        }
    }
}
