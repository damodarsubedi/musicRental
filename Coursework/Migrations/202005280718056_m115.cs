namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m115 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Loans", "DueDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Loans", "DueDate");
        }
    }
}
