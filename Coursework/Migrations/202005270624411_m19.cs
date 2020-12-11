namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m19 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(),
                        Email = c.String(nullable: false, maxLength: 50),
                        Password = c.String(),
                        Dob = c.DateTime(nullable: false),
                        PhoneNo = c.String(),
                        MemberCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberID)
                .ForeignKey("dbo.MemberCategories", t => t.MemberCategoryId, cascadeDelete: true)
                .Index(t => t.MemberCategoryId);
            
            CreateTable(
                "dbo.MemberCategories",
                c => new
                    {
                        MemberCategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        Description = c.String(),
                        LoanAvailable = c.Int(nullable: false),
                        FinePerExtraDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MemberCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "MemberCategoryId", "dbo.MemberCategories");
            DropIndex("dbo.Members", new[] { "MemberCategoryId" });
            DropTable("dbo.MemberCategories");
            DropTable("dbo.Members");
        }
    }
}
