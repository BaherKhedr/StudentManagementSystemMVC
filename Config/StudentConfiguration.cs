using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagementSystemMVC.Models;

namespace StudentManagementSystemMVC.Config
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();

        }
    }
}
