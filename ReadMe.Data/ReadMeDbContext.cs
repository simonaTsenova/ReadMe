using Microsoft.AspNet.Identity.EntityFramework;
using ReadMe.Models;
using System.Data.Entity;

namespace ReadMe.Data
{
    public partial class ReadMeDbContext : IdentityDbContext<User>
    {
        public ReadMeDbContext()
            : base("ReadMe", throwIfV1Schema: false)
        {
            Database.SetInitializer<ReadMeDbContext>(null);
        }

        public static ReadMeDbContext Create()
        {
            return new ReadMeDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // TODO
        }
    }

}
