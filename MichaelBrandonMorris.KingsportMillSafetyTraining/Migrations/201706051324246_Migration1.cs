using System.Data.Entity.Migrations;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Migrations
{
    public partial class Migration1 : DbMigration
    {
        public override void Down()
        {
            DropForeignKey("dbo.Slides", "Category_Id", "dbo.Categories");
            DropForeignKey(
                "dbo.RoleCategories",
                "Category_Id",
                "dbo.Categories");
            DropForeignKey("dbo.RoleCategories", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Answers", "Slide_Id", "dbo.Slides");
            DropIndex(
                "dbo.RoleCategories",
                new[]
                {
                    "Category_Id"
                });
            DropIndex(
                "dbo.RoleCategories",
                new[]
                {
                    "Role_Id"
                });
            DropIndex(
                "dbo.Slides",
                new[]
                {
                    "Category_Id"
                });
            DropIndex(
                "dbo.Answers",
                new[]
                {
                    "Slide_Id"
                });
            DropTable("dbo.RoleCategories");
            DropTable("dbo.Roles");
            DropTable("dbo.Categories");
            DropTable("dbo.Slides");
            DropTable("dbo.Answers");
        }

        public override void Up()
        {
            CreateTable(
                    "dbo.Answers",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Index = c.Int(false),
                        Title = c.String(false),
                        Slide_Id = c.Int()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Slides", t => t.Slide_Id)
                .Index(t => t.Slide_Id);

            CreateTable(
                    "dbo.Slides",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Content = c.String(false),
                        CorrectAnswerIndex = c.Int(false),
                        ImageBytes = c.Binary(),
                        ImageDescription = c.String(),
                        Index = c.Int(false),
                        Question = c.String(),
                        ShouldShowImageOnQuiz = c.Boolean(false),
                        ShouldShowQuestionOnQuiz = c.Boolean(false),
                        ShouldShowSlideInSlideshow = c.Boolean(false),
                        Title = c.String(false),
                        Category_Id = c.Int()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .Index(t => t.Category_Id);

            CreateTable(
                    "dbo.Categories",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Description = c.String(),
                        Title = c.String(),
                        Index = c.Int(false)
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.Roles",
                    c => new
                    {
                        Id = c.Int(false, true),
                        Description = c.String(),
                        Title = c.String()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.RoleCategories",
                    c => new
                    {
                        Role_Id = c.Int(false),
                        Category_Id = c.Int(false)
                    })
                .PrimaryKey(
                    t => new
                    {
                        t.Role_Id,
                        t.Category_Id
                    })
                .ForeignKey("dbo.Roles", t => t.Role_Id, true)
                .ForeignKey("dbo.Categories", t => t.Category_Id, true)
                .Index(t => t.Role_Id)
                .Index(t => t.Category_Id);
        }
    }
}