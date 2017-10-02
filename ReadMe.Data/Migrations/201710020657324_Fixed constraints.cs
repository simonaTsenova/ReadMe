namespace ReadMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixedconstraints : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserBooks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropIndex("dbo.Users", "IX_FirstNameLastName");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Users", "FirstName", c => c.String(maxLength: 15));
            AlterColumn("dbo.Users", "LastName", c => c.String(maxLength: 20));
            AddPrimaryKey("dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UserBooks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ratings", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Reviews", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.Ratings", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserBooks", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "LastName", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false, maxLength: 15));
            AlterColumn("dbo.Users", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.Users", new[] { "FirstName", "LastName" }, unique: true, name: "IX_FirstNameLastName");
            AddForeignKey("dbo.Reviews", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Ratings", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.UserBooks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users", "Id");
        }
    }
}
