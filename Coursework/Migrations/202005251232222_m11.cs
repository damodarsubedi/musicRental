namespace Coursework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumArtists",
                c => new
                    {
                        AlbumArtistId = c.Int(nullable: false, identity: true),
                        ArtistId = c.Int(nullable: false),
                        AlbumId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumArtistId)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.AlbumCopies",
                c => new
                    {
                        AlbumCopyId = c.Int(nullable: false, identity: true),
                        IssueDate = c.DateTime(),
                        DvdCopyNumber = c.String(),
                        AlbumId = c.Int(nullable: false),
                        CopyStatus = c.String(),
                    })
                .PrimaryKey(t => t.AlbumCopyId)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.AlbumProducers",
                c => new
                    {
                        AlbumProducerId = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        ProducerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumProducerId)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .Index(t => t.AlbumId)
                .Index(t => t.ProducerId);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        ProducerId = c.Int(nullable: false, identity: true),
                        ProducerName = c.String(nullable: false, maxLength: 50),
                        ProducerAddress = c.String(nullable: false, maxLength: 50),
                        ProducerPhone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProducerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlbumProducers", "ProducerId", "dbo.Producers");
            DropForeignKey("dbo.AlbumProducers", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.AlbumCopies", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.AlbumArtists", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.AlbumArtists", "AlbumId", "dbo.Albums");
            DropIndex("dbo.AlbumProducers", new[] { "ProducerId" });
            DropIndex("dbo.AlbumProducers", new[] { "AlbumId" });
            DropIndex("dbo.AlbumCopies", new[] { "AlbumId" });
            DropIndex("dbo.AlbumArtists", new[] { "AlbumId" });
            DropIndex("dbo.AlbumArtists", new[] { "ArtistId" });
            DropTable("dbo.Producers");
            DropTable("dbo.AlbumProducers");
            DropTable("dbo.AlbumCopies");
            DropTable("dbo.AlbumArtists");
        }
    }
}
