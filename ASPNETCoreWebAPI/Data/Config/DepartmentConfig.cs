using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASPNETCoreWebAPI.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(t => t.Id); // primary key

            builder.Property(x => x.Id).UseIdentityColumn(); // auto generated

            builder.Property(n => n.DepartmetnName).IsRequired().HasMaxLength(200);
            builder.Property(n => n.Description).IsRequired(false).HasMaxLength(500);

            builder.HasData(new List<Department>()
            {
                new Department
                {
                    Id = 1,
                    DepartmetnName = "ECE",
                    Description = "ECE Department"
                },
                new Department
                {
                    Id = 2,
                    DepartmetnName = "CSE",
                    Description = "CSE Department"
                }
            });
        }
    }
}
