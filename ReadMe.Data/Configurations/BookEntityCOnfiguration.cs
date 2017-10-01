using ReadMe.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace ReadMe.Data.Configurations
{
    public class BookEntityConfiguration : EntityTypeConfiguration<Book>
    {
        public BookEntityConfiguration()
        {
            this.Property(book => book.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(book => book.Title)
                .IsRequired()
                .HasMaxLength(60)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_BookTitle") { IsUnique = true }));

            this.Property(book => book.Published)
                .IsRequired();

            this.Property(book => book.ISBN)
                .IsRequired()
                .HasMaxLength(13)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_BookISBN") { IsUnique = true }));

            this.Property(book => book.Summary)
                .HasColumnType("ntext");
        }
    }
}
