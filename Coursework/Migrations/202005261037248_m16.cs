namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "AgeRestricted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "AgeRestricted");
        }
    }
}
