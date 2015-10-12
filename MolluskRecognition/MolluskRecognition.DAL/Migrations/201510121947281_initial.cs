namespace MolluskRecognition.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MolluskCollections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        SampleNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Families",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                        Feature_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Features", t => t.Feature_Id)
                .Index(t => t.Feature_Id);
            
            CreateTable(
                "dbo.Genus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Year = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Year = c.DateTime(nullable: false),
                        Age = c.String(),
                        Synonymy = c.String(),
                        Shell = c.String(),
                        InitialShellTypeDescription = c.String(),
                        Umbones = c.String(),
                        DorsalMargin = c.String(),
                        VentralMargin = c.String(),
                        PosteriorEnd = c.String(),
                        FrontEnd = c.String(),
                        Sculpture = c.String(),
                        Comparison = c.String(),
                        InitialShellType = c.Int(nullable: false),
                        SculptureType = c.Int(nullable: false),
                        Genus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genus", t => t.Genus_Id)
                .Index(t => t.Genus_Id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Species_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Species", t => t.Species_Id)
                .Index(t => t.Species_Id);
            
            CreateTable(
                "dbo.Samples",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        L = c.Double(nullable: false),
                        LSmall = c.Double(nullable: false),
                        L1 = c.Double(nullable: false),
                        L2 = c.Double(nullable: false),
                        H = c.Double(nullable: false),
                        D = c.Double(nullable: false),
                        AngleBetta = c.Double(nullable: false),
                        AngleAlfa = c.Double(nullable: false),
                        PhotoFileName = c.String(),
                        CollectionLocation_Id = c.Int(),
                        Species_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MolluskCollections", t => t.CollectionLocation_Id)
                .ForeignKey("dbo.Species", t => t.Species_Id)
                .Index(t => t.CollectionLocation_Id)
                .Index(t => t.Species_Id);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Species_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Species", t => t.Species_Id)
                .Index(t => t.Species_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Species", "Genus_Id", "dbo.Genus");
            DropForeignKey("dbo.Sections", "Species_Id", "dbo.Species");
            DropForeignKey("dbo.Samples", "Species_Id", "dbo.Species");
            DropForeignKey("dbo.Samples", "CollectionLocation_Id", "dbo.MolluskCollections");
            DropForeignKey("dbo.Locations", "Species_Id", "dbo.Species");
            DropForeignKey("dbo.Features", "Feature_Id", "dbo.Features");
            DropIndex("dbo.Sections", new[] { "Species_Id" });
            DropIndex("dbo.Samples", new[] { "Species_Id" });
            DropIndex("dbo.Samples", new[] { "CollectionLocation_Id" });
            DropIndex("dbo.Locations", new[] { "Species_Id" });
            DropIndex("dbo.Species", new[] { "Genus_Id" });
            DropIndex("dbo.Features", new[] { "Feature_Id" });
            DropTable("dbo.Sections");
            DropTable("dbo.Samples");
            DropTable("dbo.Locations");
            DropTable("dbo.Species");
            DropTable("dbo.Genus");
            DropTable("dbo.Features");
            DropTable("dbo.Families");
            DropTable("dbo.MolluskCollections");
        }
    }
}
