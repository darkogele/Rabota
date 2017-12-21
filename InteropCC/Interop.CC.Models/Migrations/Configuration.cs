using System;
using System.Linq;
using Interop.CC.Models.DTO;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interop.CC.Models.Migrations
{
    using Models;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<InteropContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(InteropContext context)
        {

            #region Services

            context.Services.AddOrUpdate(
                s => s.Code,
                new Service
                {
                    Code = "EvalService",
                    Name = "EvalService",
                    Endpoint = "http://localhost/evalservicesite/eval.svc/custom",
                    Wsdl = "WSDL example"
                });

            context.Providers.AddOrUpdate(
                s => s.RoutingToken,
                new Provider
                {
                    RoutingToken = "UJP",
                    PublicKey = "<RSAKeyValue><Modulus>ks+L8kWHiBwiPw4zJcZwIkeGrhNP0fI6LohybpGjNoZSf4bZ1hXrgLiWoklA2QY7CD7hPbW2d1cLVK7VOAYqAtyIdrchG6AVSWg2ul90QT/BgvNFcBqf9xuS3l25t1OimUcj47/hPx2Nu9NMMMpGhqp6PR2pEwjvMAxHgW7BzOM=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
                },
                new Provider
                {
                    RoutingToken = "AKN",
                    PublicKey = "<RSAKeyValue><Modulus>qusbQgzQc9kX7mz98Kvt5zMzUpOMo7+uWsGfuewti2SJFRIQv9Gbl0yELTJZUen2denEUg8Vkh8NZs+AiHKzFrDkaDveaX0f8qHW3+nAhD6UqLHNuHkoQFhcz5e0QOiNXRIBuQ73ylxFBmBnqmLNBAVsuprWD7kEfYoE/k5uHgk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>"
                });

            #endregion

            #region Clients

            context.Clients.AddOrUpdate(
                p => p.Id,
                new Client
                {
                    Id = "consoleApp",
                    Active = true,
                    Secret = Helper.Helper.GetHash("abc@123"),
                    ApplicationType = ApplicationTypes.NativeConfidential,
                    Name = "Console Application",
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "*"
                },
                new Client
                {
                    Id = "ngAuthApp",
                    Active = true,
                    Secret = Helper.Helper.GetHash("123@abc"),
                    ApplicationType = ApplicationTypes.JavaScript,
                    Name = "AngularJS front-end Application",
                    RefreshTokenLifeTime = 7200,
#if DEBUG
                    AllowedOrigin = "http://localhost/interop.cs.portal.ui",
#else
                    AllowedOrigin = "https://localhost/interop.cs.portal.ui",
#endif
                });

            #endregion

            #region MessageLogs

            context.MessageLogs.AddOrUpdate(
                p => p.Id,
                new MessageLog
                {
                    Id = 1,
                    Consumer = "Consumer",
                    Provider = "Provider",
                    RoutingToken = "RoutingToken",
                    Service = "Service",
                    TransactionId = Guid.NewGuid(),
                    Dir = "Request",
                    CallType = "async",
                    PublicKey = "PublicKey",
                    Status = "Status",
                    MimeType = "MimeType",
                    Timestamp = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Signature = "Signature",
                    CorrelationId = "CorelationId",
                },
                new MessageLog
                {
                    Id = 2,
                    Consumer = "Consumer",
                    Provider = "Provider",
                    RoutingToken = "RoutingToken",
                    Service = "Service",
                    TransactionId = Guid.NewGuid(),
                    Dir = "Request",
                    CallType = "async",
                    PublicKey = "PublicKey",
                    Status = "Status",
                    MimeType = "MimeType",
                    Timestamp = DateTime.Now,
                    CreateDate = DateTime.Now,
                    Signature = "Signature",
                    CorrelationId = "CorelationId"
                });

            #endregion

            #region SoapFaults

            context.SoapFaults.AddOrUpdate(
                p => p.TransactionId,
                new SoapFault
                {
                    TransactionId = Guid.NewGuid(),
                    Code = "Code1",
                    SubCode = "SubCode1",
                    Reason = "Reason1",
                    Details = "Details1",
                    DateOccured = DateTime.Now,
                    DateCreated = DateTime.Now
                },
                new SoapFault
                {
                    TransactionId = Guid.NewGuid(),
                    Code = "Code2",
                    SubCode = "SubCode2",
                    Reason = "Reason2",
                    Details = "Details2",
                    DateOccured = DateTime.Now,
                    DateCreated = DateTime.Now
                });

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

            context.SaveChanges();
        }
    }
}
