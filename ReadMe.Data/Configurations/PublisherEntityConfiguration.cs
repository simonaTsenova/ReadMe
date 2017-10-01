using ReadMe.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace ReadMe.Data.Configurations
{
    public class PublisherEntityConfiguration : EntityTypeConfiguration<Publisher>
    {
        public PublisherEntityConfiguration()
        {
            this.Property(publisher => publisher.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(publisher => publisher.Name)
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IX_PublisherName") { IsUnique = true }));

            this.Property(publisher => publisher.Owner)
                .HasMaxLength(40);

            this.Property(publisher => publisher.Address)
                .HasMaxLength(40);
        }
    }
}
