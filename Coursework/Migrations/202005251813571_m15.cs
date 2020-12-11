namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m15 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AlbumTypes", "AgeRestricted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AlbumTypes", "AgeRestricted", c => c.Boolean(nullable: false));
        }
    }
}
