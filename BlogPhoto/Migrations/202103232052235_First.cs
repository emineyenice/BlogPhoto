namespace BlogPhoto.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HashTags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HashTagName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhotoTitle = c.String(),
                        Description = c.String(),
                        LoadTime = c.DateTime(nullable: false),
                        HashTag = c.String(),
                        ImagePath = c.String(),
                        HashTagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PhotoHashTags",
                c => new
                    {
                        Photo_Id = c.Int(nullable: false),
                        HashTag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Photo_Id, t.HashTag_Id })
                .ForeignKey("dbo.Photos", t => t.Photo_Id, cascadeDelete: true)
                .ForeignKey("dbo.HashTags", t => t.HashTag_Id, cascadeDelete: true)
                .Index(t => t.Photo_Id)
                .Index(t => t.HashTag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PhotoHashTags", "HashTag_Id", "dbo.HashTags");
            DropForeignKey("dbo.PhotoHashTags", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.PhotoHashTags", new[] { "HashTag_Id" });
            DropIndex("dbo.PhotoHashTags", new[] { "Photo_Id" });
            DropTable("dbo.PhotoHashTags");
            DropTable("dbo.Photos");
            DropTable("dbo.HashTags");
        }
    }
}
