using System;
using System.Linq;
using Interop.CS.Models.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interop.CS.Models.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Interop.CS.Models.InteropContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Interop.CS.Models.InteropContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            #region Message logs
            context.MessageLogs.AddOrUpdate(
                m => m.Id,
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() },
                new MessageLog { CallType = "CallType1", Consumer = "Consumer1", CorrelationId = "CorrelationId1", CreateDate = DateTime.Now, Dir = "Dir1", MimeType = "MimeType1", Provider = "Provider1", PublicKey = "PublicKey1", RoutingToken = "RoutingToken1", Service = "Service1", ServiceMethod = "ServiceMethod1", Signature = "Signature1", Status = "Status1", Timestamp = DateTime.Now, TransactionId = new Guid() }
                );
            #endregion

            #region Participants
            context.Participants.AddOrUpdate(
                p => p.Code,
                new Participant { Code = "KIT", Name = "KIT", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "OKP", Name = "OKP", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "ZAG", Name = "ZAG", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "TAR", Name = "TAR", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "MVR", Name = "MVR", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "UJP", Name = "UJP", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "MAR", Name = "MAR", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "GEM", Name = "GEM", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "ZDR", Name = "ZDR", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "ZEM", Name = "ZEM", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "KOR", Name = "KOR", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "KNG", Name = "KNG", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "BEN", Name = "BEN", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "KITT", Name = "KIT", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "OKPT", Name = "OKP", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "ZAGT", Name = "ZAG", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "TART", Name = "TAR", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "MVRT", Name = "MVR", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "UJPT", Name = "UJP", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "MART", Name = "MAR", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "GEMT", Name = "GEM", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "ZDRT", Name = "ZDR", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "ZEMT", Name = "ZEM", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "KORT", Name = "KOR", Uri = "/", IsActive = true, PublicKey = "PK" },
                new Participant { Code = "KNGT", Name = "KNG", Uri = "/", IsActive = false, PublicKey = "PK" },
                new Participant { Code = "BENT", Name = "BEN", Uri = "/", IsActive = true, PublicKey = "PK" }
                );
            #endregion

            #region Service
            context.Services.AddOrUpdate(p => new { p.Code, p.ParticipantCode },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "KIT", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "KIT", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "OKP", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "OKP", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "ZAG", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "ZAG", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "TAR", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "TAR", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "MVR", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "MVR", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "UJP", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "UJP", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "MAR", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "MAR", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "GEM", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "GEM", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "ZDR", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "ZDR", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "ZEM", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "ZEM", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "KOR", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "KOR", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "KNG", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "KNG", Wsdl = "/" },
                new CSService { Code = "ServiceA", Name = "ServiceA", ParticipantCode = "BEN", Wsdl = "/" },
                new CSService { Code = "ServiceB", Name = "ServiceB", ParticipantCode = "BEN", Wsdl = "/" }
                );
            #endregion

            #region AccessMappings
            context.AccessMappings.AddOrUpdate(
               p => new { p.ProviderCode, p.ConsumerCode, p.ServiceCode },
                new AccessMapping { ProviderCode = "KOR", ConsumerCode = "BEN", ServiceCode = "ServiceA", ProviderBusCode = "MIM1", ConsumerBusCode = "MIM1", MethodCode = "X" },
                new AccessMapping { ProviderCode = "KOR", ConsumerCode = "MAR", ServiceCode = "ServiceB", ProviderBusCode = "MIM1", ConsumerBusCode = "MIM2", MethodCode = "X" },
                new AccessMapping { ProviderCode = "BEN", ConsumerCode = "OKP", ServiceCode = "ServiceA", ProviderBusCode = "MIM1", ConsumerBusCode = "MIM1", MethodCode = "X" },
                new AccessMapping { ProviderCode = "ZDR", ConsumerCode = "MVR", ServiceCode = "ServiceB", ProviderBusCode = "MIM2", ConsumerBusCode = "MIM1", MethodCode = "X" }
            );
            #endregion

            #region Clients

            context.Clients.AddOrUpdate(p => p.Id,
                new Client
                {
                    Id = "consoleApp",
                    Secret = Helper.GetHash("abc@123"),
                    Name = "Console Application",
                    ApplicationType = ApplicationTypes.NativeConfidential,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                },
                new Client
                {
                    Id = "ngAuthApp",
                    Secret = Helper.GetHash("123@abc"),
                    Name = "AngularJS front-end Application",
                    ApplicationType = ApplicationTypes.Javascript,
                    Active = true,
                    RefreshTokenLifeTime = 7200,
#if DEBUG
                    AllowedOrigin = "http://localhost/interop.cs.portal.ui",
#else
                    AllowedOrigin = "https://localhost/interop.cs.portal.ui",
#endif
                }
                );
            #endregion

            #region Identity Users & Roles

            if (!context.Roles.Any(r => r.Name == "SuperAdmin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var roleSuperAdmin = new IdentityRole { Name = "SuperAdmin" };

                manager.Create(roleSuperAdmin);
            }

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var roleAdmin = new IdentityRole { Name = "Admin" };

                manager.Create(roleAdmin);
            }

            if (!context.Roles.Any(r => r.Name == "User"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var roleUser = new IdentityRole { Name = "User" };

                manager.Create(roleUser);
            }

            if (!context.Users.Any(u => u.UserName == "KorvusAdmin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var userSuperAdmin = new ApplicationUser { UserName = "KorvusAdmin", PublicKey = "testkey" };

                manager.Create(userSuperAdmin, "KorvInterop2015");
                manager.AddToRole(userSuperAdmin.Id, "SuperAdmin");
            }

            if (!context.Users.Any(u => u.UserName == "MIOAAdmin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var userAdmin = new ApplicationUser { UserName = "MIOAAdmin", PublicKey = "testkey" };

                manager.Create(userAdmin, "MioaInterop2015");
                manager.AddToRole(userAdmin.Id, "Admin");
            }

            #endregion

            #region Message logs

            context.MessageLogStatistic.AddOrUpdate(mls => mls.Id,
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 10, 16, 13, 53, 42, 287),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "http://localhost/Interop.CC.Handler.Internal",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("F147756E-F67F-49C5-ABCE-BFC42420CE65"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 10, 16, 13, 53, 18, 543),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 10, 16, 13, 54, 35, 387),
                    Dir = "Response",
                    MimeType = "application/soap+xml; charset=utf-8",
                    ParticipantUri = "http://localhost/Interop.CC.Handler.Internal",
                    Provider = "AKN",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("F147756E-F67F-49C5-ABCE-BFC42420CE65"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 10, 16, 13, 54, 35, 363),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 12, 11, 13, 55, 09, 477),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "http://localhost/Interop.CC.Handler.Internal",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("AF4F92AB-75A5-4EDB-9C1E-AA9D7B58FE46"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 12, 11, 13, 55, 06, 350),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 10, 16, 13, 53, 18, 650),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "http://localhost/Interop.CCSimulation.Handler.Internal",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("F147756E-F67F-49C5-ABCE-BFC42420CE65"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 10, 16, 13, 53, 18, 543),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 10, 16, 13, 54, 37, 873),
                    Dir = "Response",
                    MimeType = "application/soap+xml; charset=utf-8",
                    ParticipantUri = "http://localhost/Interop.CCSimulation.Handler.Internal",
                    Provider = "AKN",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("F147756E-F67F-49C5-ABCE-BFC42420CE65"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 10, 16, 13, 54, 35, 363),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 12, 11, 13, 55, 06, 447),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "http://localhost/Interop.CCSimulation.Handler.Internal",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("AF4F92AB-75A5-4EDB-9C1E-AA9D7B58FE46"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 12, 11, 13, 55, 06, 350),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 12, 11, 13, 55, 06, 447),
                    Dir = "Response",
                    MimeType = "application/soap+xml; charset=utf-8",
                    ParticipantUri = "http://localhost/Interop.CCSimulation.Handler.Internal",
                    Provider = "AKN",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("AF4F92AB-75A5-4EDB-9C1E-AA9D7B58FE46"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 12, 11, 13, 55, 06, 350),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 10, 16, 13, 53, 18, 650),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "CS",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("F147756E-F67F-49C5-ABCE-BFC42420CE65"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 10, 16, 13, 53, 18, 543),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2016, 10, 16, 13, 54, 37, 873),
                    Dir = "Response",
                    MimeType = "application/soap+xml; charset=utf-8",
                    ParticipantUri = "CS",
                    Provider = "AKN",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("F147756E-F67F-49C5-ABCE-BFC42420CE65"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2016, 10, 16, 13, 54, 35, 363),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2016, 10, 16, 13, 54, 37, 873),
                    Dir = "Response",
                    MimeType = "application/soap+xml; charset=utf-8",
                    ParticipantUri = "CS",
                    Provider = "AKN",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("F147756E-F67F-49C5-ABCE-BFC42420CE65"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2016, 10, 16, 13, 54, 35, 363),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 03, 01, 13, 32, 35, 640),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "CS",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("3F988A5E-2A69-430D-BDCD-345E0967A8FA"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 03, 01, 13, 32, 30, 530),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 03, 01, 13, 32, 35, 640),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "http://localhost/Interop.CC.Handler.Internal",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("3F988A5E-2A69-430D-BDCD-345E0967A8FA"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 03, 01, 13, 32, 30, 530),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 03, 01, 13, 32, 35, 640),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "http://localhost/Interop.CCSimulation.Handler.Internal",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("3F988A5E-2A69-430D-BDCD-345E0967A8FA"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 03, 01, 13, 32, 30, 530),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 03, 01, 13, 32, 35, 640),
                    Dir = "Response",
                    MimeType = "application/soap+xml; charset=utf-8",
                    ParticipantUri = "http://localhost/Interop.CC.Handler.Internal",
                    Provider = "AKN",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("3F988A5E-2A69-430D-BDCD-345E0967A8FA"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 03, 01, 13, 32, 30, 530),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 03, 01, 13, 32, 35, 640),
                    Dir = "Response",
                    MimeType = "application/soap+xml; charset=utf-8",
                    ParticipantUri = "CS",
                    Provider = "AKN",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("3F988A5E-2A69-430D-BDCD-345E0967A8FA"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 03, 01, 13, 32, 30, 530),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 12, 11, 13, 55, 06, 447),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "CS",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("AF4F92AB-75A5-4EDB-9C1E-AA9D7B58FE46"),
                    RoutingToken = "AKN",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 12, 11, 13, 55, 06, 350),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "MON",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 11, 13, 00, 00, 00, 000),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "http://localhost/Interop.CC.Handler.Internal",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("8BFCB587-FEC7-4311-9C27-924FF81930A4"),
                    RoutingToken = "UJP",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 11, 13, 00, 00, 00, 000),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "MON",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 11, 13, 00, 00, 00, 000),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "CS",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("8BFCB587-FEC7-4311-9C27-924FF81930A4"),
                    RoutingToken = "UJP",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 11, 13, 00, 00, 00, 000),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 11, 13, 00, 00, 00, 000),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "http://localhost/Interop.CC.Handler.Internal",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("4D07DFAF-3B84-45A8-A2B6-D5E465F4E21D"),
                    RoutingToken = "MON",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 11, 13, 00, 00, 00, 000),
                    Signature = "",
                    Status = "EvalService"
                },
                new MessageLogStatistic
                {
                    CallType = "synchronous",
                    Consumer = "UJP",
                    CorrelationId = null,
                    CreateDate = new DateTime(2015, 11, 13, 00, 00, 00, 000),
                    Dir = "Request",
                    MimeType = "",
                    ParticipantUri = "CS",
                    Provider = "",
                    PublicKey = "TestPublicKey",
                    TransactionId = new Guid("4D07DFAF-3B84-45A8-A2B6-D5E465F4E21D"),
                    RoutingToken = "MON",
                    Service = "EvalService",
                    ServiceMethod = null,
                    Timestamp = new DateTime(2015, 11, 13, 00, 00, 00, 000),
                    Signature = "",
                    Status = "EvalService"
                }
                );
            #endregion


            context.SaveChanges();

        }
    }
}
