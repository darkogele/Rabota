namespace Interop.CC.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProvidersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        PublicKey = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Providers");
        }
    }
}
