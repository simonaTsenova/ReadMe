namespace ReadMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserusernameuniqueindexconstraint : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "UserName", c => c.String(maxLength: 40));
            CreateIndex("dbo.Users", "UserName", unique: true, name: "IX_UserUserName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", "IX_UserUserName");
            AlterColumn("dbo.Users", "UserName", c => c.String());
        }
    }
}
