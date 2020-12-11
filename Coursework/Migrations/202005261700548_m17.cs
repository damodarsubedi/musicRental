namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m17 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Studio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Albums", "Studio");
        }
    }
}
