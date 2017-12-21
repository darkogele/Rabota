namespace EvalServiceLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evals",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Submitter = c.String(nullable: false),
                        Comments = c.String(),
                        TimeSubmitted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Evals");
        }
    }
}
