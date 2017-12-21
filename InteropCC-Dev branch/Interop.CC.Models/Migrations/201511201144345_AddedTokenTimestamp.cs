namespace Interop.CC.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTokenTimestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MessageLogs", "TokenTimestamp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MessageLogs", "TokenTimestamp");
        }
    }
}
