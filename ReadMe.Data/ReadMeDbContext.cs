﻿using Microsoft.AspNet.Identity.EntityFramework;
using ReadMe.Data.Configurations;
using ReadMe.Data.Contracts;
using ReadMe.Data.Migrations;
using ReadMe.Models;
using System.Data.Entity;

namespace ReadMe.Data
{
    public partial class ReadMeDbContext : IdentityDbContext<User>, IReadMeDbContext
    {
        public ReadMeDbContext()
            : base("LocalReadMeConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ReadMeDbContext, Configuration>());
        }

        public static ReadMeDbContext Create()
        {
            return new ReadMeDbContext();
        }

        //public override int SaveChanges()
        //{
        //    return base.SaveChanges();
        //}

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

        public IDbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void Add<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Added;
        }

        public void Delete<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void Update<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }

}
