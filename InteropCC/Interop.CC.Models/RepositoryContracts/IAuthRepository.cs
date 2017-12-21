using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Interop.CC.Models.RepositoryContracts
{
    public interface IAuthRepository : IDisposable
    {
        // ASP.NET Identity
        PagedCollection<UserViewModel> GetAllUsers(int pageIndex, int itemsPerPage, string sortDir,
            string sortCol, string userName, string ddlRole, IPrincipal loggedInUser);
        List<RoleViewModel> GetUserRoles(IPrincipal currentUser, List<string> userRoles);
        List<string> GetUserRoles();
        List<RoleViewModel> GetUserRolesRegister(IPrincipal loggedUser);
        List<RoleViewModel> GetServiceRoles(List<string> userRoles);
        List<string> GetServiceRolesRegister();
        void DeleteUser(ApplicationUser user, ApplicationUser loggedUser);
        void DeleteRole(ApplicationUser user, IdentityUserRole role);
        void CreateRole(List<string> rolesToCreate);
        void RegisterUser(UserModelDTO userModel);
        ApplicationUser UpdateUser(ApplicationUser user, ApplicationUser loggedUser);
        Task<ApplicationUser> FindUser(string userName, string password);
        Task<bool> FindUserInUsers(string userName);
        Task<List<string>> FindRoleAsync(string userId);
        Task<bool> FindRoleInRoles(string role);
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login);
        IQueryable<ApplicationUser> GetUsers();
        ApplicationUser GetUser(string id);

        // RefreshToken & OWIN
        Client FindClient(string clientId);
        Task<bool> AddRefreshToken(RefreshToken token);
        Task<bool> RemoveRefreshToken(string refreshTokenId);
        Task<bool> RemoveRefreshToken(RefreshToken refreshToken);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        List<RefreshToken> GetAllRefreshTokens();
        List<IdentityRoleModel> GetRoles();
        void DeleteIdentityRole(string roleName);
        void ChangePassword(ApplicationUser user);
    }
}
