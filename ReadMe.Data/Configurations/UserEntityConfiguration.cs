using ReadMe.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ReadMe.Data.Configurations
{
    public class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            this.Property(user => user.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(user => user.FirstName)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_FirstNameLastName", 1) { IsUnique = true }));

            this.Property(user => user.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_FirstNameLastName", 2) { IsUnique = true }));
        }
    }
}
