namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        ReleasedDate = c.DateTime(nullable: false),
                        NoofSongs = c.Int(nullable: false),
                        TotalLength = c.Int(nullable: false),
                        CopyNumber = c.Int(nullable: false),
                        StandardCharge = c.Int(nullable: false),
                        CoverImagePath = c.String(),
                        AlbumTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.AlbumTypes", t => t.AlbumTypeId, cascadeDelete: true)
                .Index(t => t.AlbumTypeId);
            
            CreateTable(
                "dbo.AlbumTypes",
                c => new
                    {
                        AlbumTypeId = c.Int(nullable: false, identity: true),
                        AlbumTypeName = c.String(),
                        AgeRestricted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumTypeId);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Gender = c.String(),
                        Email = c.String(),
                        ContactNumber = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ArtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Albums", "AlbumTypeId", "dbo.AlbumTypes");
            DropIndex("dbo.Albums", new[] { "AlbumTypeId" });
            DropTable("dbo.Artists");
            DropTable("dbo.AlbumTypes");
            DropTable("dbo.Albums");
        }
    }
}
