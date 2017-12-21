using System.Data.Entity;
using System.Threading.Tasks;
using Interop.CC.Models.Models;

namespace Interop.CC.Models
{
    public interface IInteropContext
    {
        DbSet<MessageLog> MessageLogs { get; set; }
        DbSet<Service> Services { get; set; }
        DbSet<Client> Clients { get; set; } 
        DbSet<Provider> Providers { get; set; }
        DbSet<SoapFault> SoapFaults { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
        void Dispose();
    }
}
