using ReadMe.Models;
using System.Data.Entity.ModelConfiguration;

namespace ReadMe.Data.Configurations
{
    public class UserBooksEntityConfiguration : EntityTypeConfiguration<UserBook>
    {
        public UserBooksEntityConfiguration()
        {
            this.Property(userBooks => userBooks.ReadStatus)
                .IsRequired();
        }
    }
}
