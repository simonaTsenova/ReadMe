using ReadMe.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ReadMe.Data.Configurations
{
    public class RatingEntityConfiguration : EntityTypeConfiguration<Rating>
    {
        public RatingEntityConfiguration()
        {
            this.Property(rating => rating.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(rating => rating.Stars)
                .IsRequired();
        }
    }
}
