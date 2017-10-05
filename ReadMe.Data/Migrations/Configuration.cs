namespace ReadMe.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ReadMe.Models;
    using ReadMe.Models.Enumerations;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ReadMe.Data.ReadMeDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(ReadMeDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            this.SeedAdmin(context);
            this.SeedGenres(context);
            this.SeedAuthors(context);
            this.SeedPublishers(context);
            this.SeedBooks(context);
            this.SeedUserBooks(context);
        }

        private void SeedGenres(ReadMeDbContext context)
        {
            if (!context.Genres.Any())
            {
                var genres = new Genre[]
                {
                    new Genre("Fantasy"),
                    new Genre("Romance"),
                    new Genre("Mystery"),
                    new Genre("Thriller")
                };

                foreach (var genre in genres)
                {
                    context.Genres.Add(genre);
                }
            }
        }

        private void SeedAuthors(ReadMeDbContext context)
        {
            if(!context.Authors.Any())
            {
                var authors = new Author[]
                {
                    new Author("John", "Grisham", "American", 54, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", "http://www.jgrisham.com/"),
                    new Author("Danielle", "Steel", "American", 56, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.", "http://daniellesteel.com")
                };

                foreach (var author in authors)
                {
                    context.Authors.Add(author);
                }
            }
        }
        
        private void SeedPublishers(ReadMeDbContext context)
        {
            if(!context.Publishers.Any())
            {
                var publishers = new Publisher[]
                {
                    new Publisher("Paradise press", "John Doe", "1234567899", "California", "35 Hollywood blvd., LA", "USA", "wwww.website.com"),
                    new Publisher("Bear press", "Jane Doe", "9876543211", "Chicago", "48 Franklin str.", "USA", "www.website.com")
                };

                foreach (var publisher in publishers)
                {
                    context.Publishers.Add(publisher);
                }
            }
        }

        private void SeedBooks(ReadMeDbContext context)
        {
            if(!context.Books.Any())
            {
                var date = DateTime.Now;
                var books = new Book[]
                {
                    new Book("Sisters", date, "0385340222",
                        context.Authors.First(x => (x.FirstName == "Danielle" && x.LastName == "Steel")),
                        "Four sisters, a Manhattan brownstone, and a tumultuous year of loss and courage are at the heart of Danielle Steel's new novel about a remarkable family, a stunning tragedy--and what happens when four very different young women come together under one very lively roof.",
                        "English", context.Publishers.First(x => x.Name == "Paradise press"),
                        new Genre[]{ context.Genres.First(x => x.Name == "Romance")}
                        ),
                    new Book("The Rooster Bar", date, "0385541171",
                        context.Authors.First(x => (x.FirstName == "John" && x.LastName == "Grisham")),
                        "Mark, Todd, and Zola came to law school to change the world, to make it a better place. But now, as third-year students, these close friends realize they have been duped.",
                        "English", context.Publishers.First(x => x.Name == "Paradise press"),
                        new Genre[]{ context.Genres.First(x => x.Name == "Thriller"), context.Genres.First(x => x.Name == "Mystery") }
                        ),

                };

                foreach (var book in books)
                {
                    context.Books.Add(book);
                }
            }
        }

        private void SeedUserBooks(ReadMeDbContext context)
        {
            if(!context.UserBooks.Any())
            {
                var userBook1 = new UserBook();
                userBook1.Book = context.Books.FirstOrDefault(b => b.Title == "Sisters");
                userBook1.User = context.Users.FirstOrDefault(u => u.UserName == "simona");
                userBook1.ReadStatus = ReadStatus.Read;

                var userBook2 = new UserBook();
                userBook2.Book = context.Books.FirstOrDefault(b => b.Title == "The Rooster Bar");
                userBook2.User = context.Users.FirstOrDefault(u => u.UserName == "simona");
                userBook2.ReadStatus = ReadStatus.WantToRead;

                var userBooks = new UserBook[]
                {
                    userBook1, userBook2
                };

                foreach (var userBook in userBooks)
                {
                    context.UserBooks.Add(userBook);
                }
            }
        }

        private void SeedAdmin(ReadMeDbContext context)
        {
            const string AdministratorEmail = "admin@admin.com";
            const string AdministratorUserName = "admin";
            const string AdministratorPassword = "123456";
            const string AdministratorFirstName = "Admin";
            const string AdministratorLastName = "Admin";

            if (!context.Roles.Any())
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = "Admin" };
                roleManager.Create(role);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorEmail,
                    FirstName = AdministratorFirstName,
                    LastName = AdministratorLastName
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
