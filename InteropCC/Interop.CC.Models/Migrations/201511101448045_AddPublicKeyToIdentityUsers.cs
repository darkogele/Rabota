namespace Interop.CC.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPublicKeyToIdentityUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PublicKey", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PublicKey");
        }
    }
}
