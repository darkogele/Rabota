namespace Interop.CC.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialNewNameDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        ApplicationType = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                        AllowedOrigin = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MessageLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Consumer = c.String(nullable: false),
                        Provider = c.String(),
                        RoutingToken = c.String(nullable: false),
                        Service = c.String(nullable: false),
                        TransactionId = c.Guid(nullable: false),
                        Dir = c.String(nullable: false, maxLength: 50),
                        CallType = c.String(nullable: false, maxLength: 50),
                        PublicKey = c.String(),
                        Status = c.String(maxLength: 50),
                        MimeType = c.String(maxLength: 100),
                        Timestamp = c.DateTime(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Signature = c.String(nullable: false),
                        CorrelationId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefreshToken",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        ClientId = c.String(nullable: false, maxLength: 50),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Endpoint = c.String(nullable: false, maxLength: 100),
                        Wsdl = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Service");
            DropTable("dbo.RefreshToken");
            DropTable("dbo.MessageLogs");
            DropTable("dbo.Client");
        }
    }
}
