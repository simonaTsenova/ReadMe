using Microsoft.AspNet.Identity.EntityFramework;
using ReadMe.Models;
using System.Data.Entity;

namespace ReadMe.Data
{
    public partial class ReadMeDbContext : IdentityDbContext<User>
    {
        public ReadMeDbContext()
            : base("LocalReadMeConnection", throwIfV1Schema: false)
        {
        }

        public static ReadMeDbContext Create()
        {
            return new ReadMeDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // TODO
            base.OnModelCreating(modelBuilder);
        }
    }

}
