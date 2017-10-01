using Microsoft.AspNet.Identity.EntityFramework;
using ReadMe.Data.Configurations;
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

        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<UserBook> UserBooks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new AuthorEntityConfiguration());
            modelBuilder.Configurations.Add(new BookEntityConfiguration());
            modelBuilder.Configurations.Add(new GenreEntityConfiguration());
            modelBuilder.Configurations.Add(new PublisherEntityConfiguration());
            modelBuilder.Configurations.Add(new RatingEntityConfiguration());
            modelBuilder.Configurations.Add(new ReviewEntityConfiguration());
            modelBuilder.Configurations.Add(new UserEntityConfiguration());
            modelBuilder.Configurations.Add(new UserBooksEntityConfiguration());
        }
    }

}
