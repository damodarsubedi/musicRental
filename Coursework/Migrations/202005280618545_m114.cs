namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m114 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(nullable: false),
                        AlbumCopyId = c.Int(nullable: false),
                        ReturnedDate = c.DateTime(),
                        IssuedDate = c.DateTime(nullable: false),
                        TotalCharge = c.Int(nullable: false),
                        LoanTypes = c.String(),
                    })
                .PrimaryKey(t => t.LoanId)
                .ForeignKey("dbo.AlbumCopies", t => t.AlbumCopyId, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .Index(t => t.MemberId)
                .Index(t => t.AlbumCopyId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loans", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Loans", "AlbumCopyId", "dbo.AlbumCopies");
            DropIndex("dbo.Loans", new[] { "AlbumCopyId" });
            DropIndex("dbo.Loans", new[] { "MemberId" });
            DropTable("dbo.Loans");
        }
    }
}
