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
            this.Property(user => user.UserName)
                .HasMaxLength(40)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_UserUserName") { IsUnique = true }));

            this.Property(user => user.FirstName)
                .HasMaxLength(15);

            this.Property(user => user.LastName)
                .HasMaxLength(20);
        }
    }
}
