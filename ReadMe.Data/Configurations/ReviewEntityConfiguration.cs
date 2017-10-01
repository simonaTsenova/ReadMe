using ReadMe.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ReadMe.Data.Configurations
{
    public class ReviewEntityConfiguration : EntityTypeConfiguration<Review>
    {
        public ReviewEntityConfiguration()
        {
            this.Property(review => review.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(review => review.Content)
                .IsRequired()
                .HasColumnType("ntext");
        }
    }
}
