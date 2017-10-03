namespace ReadMe.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedphotourlpropertytouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PhotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PhotoUrl");
        }
    }
}
