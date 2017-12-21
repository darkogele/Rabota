namespace Interop.CC.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodeRenamed : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Providers");
            AddColumn("dbo.Providers", "RoutingToken", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Providers", "RoutingToken");
            DropColumn("dbo.Providers", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Providers", "Code", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Providers");
            DropColumn("dbo.Providers", "RoutingToken");
            AddPrimaryKey("dbo.Providers", "Code");
        }
    }
}
