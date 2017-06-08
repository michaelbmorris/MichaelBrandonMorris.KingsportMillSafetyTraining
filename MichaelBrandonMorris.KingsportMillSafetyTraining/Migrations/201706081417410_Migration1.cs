namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Title = c.String(),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        CorrectAnswerIndex = c.Int(nullable: false),
                        ImageBytes = c.Binary(),
                        ImageDescription = c.String(),
                        Index = c.Int(nullable: false),
                        Question = c.String(),
                        ShouldShowImageOnQuiz = c.Boolean(nullable: false),
                        ShouldShowQuestionOnQuiz = c.Boolean(nullable: false),
                        ShouldShowSlideInSlideshow = c.Boolean(nullable: false),
                        Title = c.String(nullable: false),
                        Category_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Index = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Slide_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Slides", t => t.Slide_Id)
                .Index(t => t.Slide_Id);
            
            CreateTable(
                "dbo.CategoryRoles",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Role_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Role_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Role_Id);
            
            AddColumn("dbo.AspNetUsers", "BirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "CompanyName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "MiddleName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Role_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Role_Id");
            AddForeignKey("dbo.AspNetUsers", "Role_Id", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Slides", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.Answers", "Slide_Id", "dbo.Slides");
            DropForeignKey("dbo.CategoryRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.CategoryRoles", "Category_Id", "dbo.Categories");
            DropIndex("dbo.CategoryRoles", new[] { "Role_Id" });
            DropIndex("dbo.CategoryRoles", new[] { "Category_Id" });
            DropIndex("dbo.Answers", new[] { "Slide_Id" });
            DropIndex("dbo.Slides", new[] { "Category_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Role_Id" });
            DropColumn("dbo.AspNetUsers", "Role_Id");
            DropColumn("dbo.AspNetUsers", "MiddleName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "CompanyName");
            DropColumn("dbo.AspNetUsers", "BirthDate");
            DropTable("dbo.CategoryRoles");
            DropTable("dbo.Answers");
            DropTable("dbo.Slides");
            DropTable("dbo.Categories");
            DropTable("dbo.Roles");
        }
    }
}
