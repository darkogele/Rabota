namespace Interop.CC.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SoapFaultAdded : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Client", newName: "Clients");
            RenameTable(name: "dbo.RefreshToken", newName: "RefreshTokens");
            RenameTable(name: "dbo.Service", newName: "Services");
            DropPrimaryKey("dbo.Clients");
            DropPrimaryKey("dbo.RefreshTokens");
            CreateTable(
                "dbo.SoapFaults",
                c => new
                    {
                        TransactionId = c.Guid(nullable: false),
                        Code = c.String(nullable: false),
                        SubCode = c.String(nullable: false),
                        Reason = c.String(nullable: false),
                        Details = c.String(nullable: false),
                        DateOccured = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId);
            
            AlterColumn("dbo.Clients", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Clients", "AllowedOrigin", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.RefreshTokens", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Clients", "Id");
            AddPrimaryKey("dbo.RefreshTokens", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RefreshTokens");
            DropPrimaryKey("dbo.Clients");
            AlterColumn("dbo.RefreshTokens", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Clients", "AllowedOrigin", c => c.String(maxLength: 100));
            AlterColumn("dbo.Clients", "Id", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.SoapFaults");
            AddPrimaryKey("dbo.RefreshTokens", "Id");
            AddPrimaryKey("dbo.Clients", "Id");
            RenameTable(name: "dbo.Services", newName: "Service");
            RenameTable(name: "dbo.RefreshTokens", newName: "RefreshToken");
            RenameTable(name: "dbo.Clients", newName: "Client");
        }
    }
}
