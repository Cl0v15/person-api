using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPICAP.Persons.Domain.Persons;

namespace TPICAP.Persons.Persistence.EntityConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(col => col.Id).ValueGeneratedOnAdd();
            builder.ToTable("Persons");

            builder
                .Property(p => p.FirstName)
                .HasMaxLength(50)
                .HasConversion(n => n.Value, n => Name.New(n));

            builder
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .HasConversion(n => n.Value, n => Name.New(n));

            builder
                .Property(p => p.DateOfBirth)
                .HasMaxLength(50)
                .HasConversion(b => b.Value, b => BirthDate.New(b));
        }
    }
}
