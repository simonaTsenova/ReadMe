using ReadMe.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ReadMe.Data.Configurations
{
    public class AuthorEntityConfiguration : EntityTypeConfiguration<Author>
    {
        public AuthorEntityConfiguration()
        {
            this.Property(author => author.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(author => author.FirstName)
                .IsRequired()
                .HasMaxLength(15)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_FirstNameLastName", 1) { IsUnique = true }));

            this.Property(author => author.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_FirstNameLastName", 2) { IsUnique = true }));

            this.Property(author => author.Biography)
                .HasColumnType("ntext");
        }
    }
}
