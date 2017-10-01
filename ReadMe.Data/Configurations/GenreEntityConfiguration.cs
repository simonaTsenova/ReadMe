using ReadMe.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ReadMe.Data.Configurations
{
    public class GenreEntityConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreEntityConfiguration()
        {
            this.Property(genre => genre.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(genre => genre.Name)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_GenreName") { IsUnique = true }));
        }
    }
}
