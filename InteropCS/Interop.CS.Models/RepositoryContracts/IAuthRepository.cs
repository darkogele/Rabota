using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Helpers;
using Interop.CS.Models.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interop.CS.Models.RepositoryContracts
{
    public interface IAuthRepository:IDisposable
    {
        // ASP.NET Identity
        Task<IdentityResult> Register(UserModelDTO userModel);
        PagedCollection<ManagerUserViewModel> GetAllUsers(int pageIndex, int itemsPerPage, string sortDir, string sortCol, IPrincipal loggedInUser);
        IQueryable<IdentityRole> GetRoles();
        List<RoleViewModel> GetRolesForCurrentUser(IPrincipal currentUser);
        ApplicationUser UpdateUser(ApplicationUser user, ApplicationUser loggedUser);
        void DeleteUser(ApplicationUser user, ApplicationUser loggedUser);
        void RegisterUser(UserModelDTO userModel);
        Task<ApplicationUser> FindUser(string userName, string password);
        Task<bool> FindUserInUsers(string userName);
        Task<string> FindRoleAsync(string userId);
        string FindRole(string userId);
        Task<bool> FindRoleInRoles(string role);
        Task<IdentityResult> CreateAsync(ApplicationUser user);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);

        IQueryable<ApplicationUser> GetUsers();

        // RefreshToken & OWIN
        Client FindClient(string clientId);
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        List<RefreshToken> GetAllRefreshTokens();
        void ChangePassword(ApplicationUser user);
    }
}
