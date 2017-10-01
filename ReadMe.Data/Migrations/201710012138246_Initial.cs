namespace ReadMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        FirstName = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Nationality = c.String(),
                        Age = c.Int(nullable: false),
                        Biography = c.String(storeType: "ntext"),
                        Website = c.String(),
                        PhotoUrl = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.FirstName, t.LastName }, unique: true, name: "IX_FirstNameLastName");
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Title = c.String(nullable: false, maxLength: 60),
                        Published = c.DateTime(nullable: false),
                        ISBN = c.String(nullable: false, maxLength: 13),
                        AuthorId = c.Guid(),
                        Summary = c.String(storeType: "ntext"),
                        Rating = c.Double(nullable: false),
                        PhotoUrl = c.String(),
                        Language = c.String(),
                        PublisherId = c.Guid(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.Title, unique: true, name: "IX_BookTitle")
                .Index(t => t.ISBN, unique: true, name: "IX_BookISBN")
                .Index(t => t.AuthorId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Name = c.String(nullable: false, maxLength: 20),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_GenreName");
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        Name = c.String(nullable: false, maxLength: 40),
                        Owner = c.String(maxLength: 40),
                        PhoneNumber = c.String(),
                        City = c.String(),
                        Address = c.String(maxLength: 40),
                        Country = c.String(),
                        Website = c.String(),
                        LogoUrl = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_PublisherName");
            
            CreateTable(
                "dbo.UserBooks",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        BookId = c.Guid(nullable: false),
                        ReadStatus = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.UserId, t.BookId })
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Nationality = c.String(),
                        Age = c.Int(nullable: false),
                        FavouriteQuote = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.FirstName, t.LastName }, unique: true, name: "IX_FirstNameLastName");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        BookId = c.Guid(),
                        UserId = c.String(maxLength: 128),
                        Stars = c.Int(nullable: false),
                        RatedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
                        BookId = c.Guid(),
                        UserId = c.String(maxLength: 128),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        PostedOn = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.GenreBooks",
                c => new
                    {
                        Genre_Id = c.Guid(nullable: false),
                        Book_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Book_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "BookId", "dbo.Books");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Ratings", "BookId", "dbo.Books");
            DropForeignKey("dbo.UserBooks", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.GenreBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.GenreBooks", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Books", "AuthorId", "dbo.Authors");
            DropIndex("dbo.GenreBooks", new[] { "Book_Id" });
            DropIndex("dbo.GenreBooks", new[] { "Genre_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropIndex("dbo.Reviews", new[] { "BookId" });
            DropIndex("dbo.Ratings", new[] { "UserId" });
            DropIndex("dbo.Ratings", new[] { "BookId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "IX_FirstNameLastName");
            DropIndex("dbo.UserBooks", new[] { "BookId" });
            DropIndex("dbo.UserBooks", new[] { "UserId" });
            DropIndex("dbo.Publishers", "IX_PublisherName");
            DropIndex("dbo.Genres", "IX_GenreName");
            DropIndex("dbo.Books", new[] { "PublisherId" });
            DropIndex("dbo.Books", new[] { "AuthorId" });
            DropIndex("dbo.Books", "IX_BookISBN");
            DropIndex("dbo.Books", "IX_BookTitle");
            DropIndex("dbo.Authors", "IX_FirstNameLastName");
            DropTable("dbo.GenreBooks");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.Ratings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserBooks");
            DropTable("dbo.Publishers");
            DropTable("dbo.Genres");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
