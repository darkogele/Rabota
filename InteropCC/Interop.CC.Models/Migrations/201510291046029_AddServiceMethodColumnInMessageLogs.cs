namespace Interop.CC.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddServiceMethodColumnInMessageLogs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageLogs", "ServiceMethod", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageLogs", "ServiceMethod");
        }
    }
}
