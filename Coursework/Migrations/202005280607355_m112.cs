namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m112 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.LoanTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LoanTypes",
                c => new
                    {
                        LoanTypeId = c.Int(nullable: false, identity: true),
                        LoanTypeNmae = c.String(),
                        ReturningDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LoanTypeId);
            
        }
    }
}
