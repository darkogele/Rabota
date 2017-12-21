using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Security.Principal;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Helper;
using Interop.CC.Models.Models;
using Interop.CC.Models.RepositoryContracts;
using Interop.CC.Models.UoW;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic;

namespace Interop.CC.Models.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUnitOfWork _uow;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        // Опис: Конструктор на MessageLogsRepository модулот 
        // Влезни параметри: модел IUnitOfWork
        public AuthRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new InteropContext()));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new InteropContext()));
        }

        private bool CanBeModified(ApplicationUser user, IPrincipal loggedInUser, List<IdentityRole> roles)
        {
            return HasCurrentUserRightsToModify(user, loggedInUser, roles);
        }

        private bool HasCurrentUserRightsToModify(ApplicationUser user, IPrincipal loggedInUser, List<IdentityRole> roles)
        {
            var superAdminRoles = roles.FirstOrDefault(x => x.Name == "SuperAdmin");
            var adminRoles = roles.FirstOrDefault(x => x.Name == "Admin");

            var userIsSuperAdmin = user.Roles.Any(x => superAdminRoles != null && x.RoleId == superAdminRoles.Id);
            var userIsAdmin = user.Roles.Any(x => adminRoles != null && x.RoleId == adminRoles.Id);
            var loggedUserIsSuperAdmin = loggedInUser.IsInRole("SuperAdmin");
            var loggedUserIsAdmin = loggedInUser.IsInRole("Admin");

            if ((loggedUserIsSuperAdmin || loggedUserIsAdmin) && userIsSuperAdmin)
            {
                return false;
            }
            if (loggedUserIsAdmin && userIsAdmin)
            {
                return false;
            }
            return true;
        }

        // Опис: Методот врши вчитување на сите корисници од база
        // Влезни параметри: податочна вредност pageIndex, itemsPerPage, sortDir, sortCol, IIdentity loggedInUser
        // Излезни параметри: PagedCollection<ManagerUserViewModel> 
        public PagedCollection<UserViewModel> GetAllUsers(int pageIndex, int itemsPerPage, string sortDir, string sortCol, string userName, string ddlRole, IPrincipal loggedInUser)
        {
            List<ApplicationUser> users = !string.IsNullOrEmpty(userName) ? GetUsers().Where(x => x.UserName.ToLower().Contains(userName.ToLower())).ToList() :
                                                                            GetUsers().Where(x => x.UserName.ToLower() != loggedInUser.Identity.Name.ToLower()).ToList();

            var allRolesInDb = _roleManager.Roles;
            var allMainRolesInDb = allRolesInDb.Where(r => r.Name == "Admin" || r.Name == "SuperAdmin" || r.Name == "User");
            var userList = new List<UserViewModel>();
            foreach (var user in users)
            {
                if (user.UserName != loggedInUser.Identity.Name)
                {
                    var allUserRoles = user.Roles.Select(m => m.RoleId).ToList();

                    var userRole = from mainRole in allMainRolesInDb
                                   join allUserRole in allUserRoles
                                       on mainRole.Id equals allUserRole
                                   select mainRole.Name;
                    var usersModel = new UserViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Role = userRole.FirstOrDefault(),
                        CanBeModified = CanBeModified(user, loggedInUser, allRolesInDb.ToList()),
                    };

                    if (string.IsNullOrEmpty(ddlRole)) ddlRole = string.Empty;
                    if (usersModel.Role.Contains(ddlRole))
                    {
                        userList.Add(usersModel);
                    }
                }
            }

            IQueryable<UserViewModel> usersPaged = userList.AsQueryable();

            // If sortCol is empty
            if (String.IsNullOrEmpty(sortCol))
            {
                sortCol = "UserName";
            }

            // If sortDir is empty
            if (String.IsNullOrEmpty(sortDir))
            {
                sortDir = "asc";
            }

            if (sortDir == "asc")
            {
                usersPaged = usersPaged.OrderBy(sortCol);
            }
            else if (sortDir == "desc")
            {
                usersPaged = usersPaged.OrderBy(sortCol + " descending");
            }
            else
            {
                usersPaged = usersPaged.OrderBy(x => x.UserName);
            }

            var pagedItems = usersPaged.Skip((pageIndex - 1) * itemsPerPage).Take(itemsPerPage).ToList();
            var totalSize = usersPaged.Count();
            return new PagedCollection<UserViewModel>(pageIndex, itemsPerPage, totalSize, pagedItems.ToList());
        }

        // Опис: Методот врши регистрирање на корисник во база
        // Влезни параметри: UserModelDTO userModel
        // Излезни параметри: /
        public void CreateRole(List<string> rolesToCreate)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new InteropContext()));
            foreach (var roleToCreate in rolesToCreate)
            {
                if (!roleManager.RoleExists(roleToCreate))
                {
                    var role = new IdentityRole(roleToCreate);
                    roleManager.Create(role);
                }
            }
        }

        public void RegisterUser(UserModelDTO userModel)
        {
#if DEBUG
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Username,
                PublicKey = "testKey",
                Email = userModel.Email
            };
#else
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Username,
                PublicKey = userModel.PublicKey,
                Email = userModel.Email
            };
#endif
            try
            {
                _userManager.UserValidator = new CustomUserValidator<ApplicationUser>(_userManager);
                _userManager.Create(user, userModel.Password);
                _userManager.AddToRole(user.Id, userModel.UserRole);
                foreach (var role in userModel.SelectedServiceRoles)
                    _userManager.AddToRole(user.Id, role);
            }
            catch (DbUpdateException ex)
            {
                SqlException s = ex.InnerException.InnerException as SqlException;
                if (s != null && s.Number == 2627)
                {
                    throw new DuplicateUserException(userModel);
                }
            }
        }

        // Опис: Методот врши бришење на корисник од база
        // Влезни параметри: ApplicationUser user
        // Излезни параметри: /
        public void DeleteUser(ApplicationUser user, ApplicationUser loggedUser)
        {
            var userRole = FindRole(user.Id);
            var loggedUserRole = FindRole(loggedUser.Id);

            if (userRole == null || loggedUserRole == null)
            {
                throw new Exception("Не е пронајдена соодветна ролја за корисникот.");
            }

            if (loggedUserRole == "Admin" && (userRole == "Admin" || userRole == "SuperAdmin"))
            {
                throw new Exception("Немате привилегија за бришење на корисникот");
            }

            try
            {
                _userManager.Delete(user);
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteRole(ApplicationUser user, IdentityUserRole role)
        {
            try
            {
                user.Roles.Remove(role);
                _uow.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // Опис: Методот врши вчитување на корисник од база
        // Влезни параметри: податочна вредност userName, password
        // Излезни параметри: Task од успешноста на резултатот
        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        // Опис: Методот врши менување на корисник во база
        // Влезни параметри: ApplicationUser user
        // Излезни параметри: Task од успешноста на резултатот за ApplicationUser
        public ApplicationUser UpdateUser(ApplicationUser user, ApplicationUser loggedUser)
        {
            var userRole = FindRole(user.Id);
            var loggedUserRole = FindRole(loggedUser.Id);

            if (userRole == null || loggedUserRole == null)
            {
                throw new Exception("Не е пронајдена соодветна ролја за корисникот.");
            }

            if (user != loggedUser)
            {
                if (loggedUserRole == "Admin" && (userRole == "Admin" || userRole == "SuperAdmin"))
                {
                    throw new Exception("Немате привилегија за менување на корисникот");
                }
            }
            try
            {
                _userManager.UserValidator = new CustomUserValidator<ApplicationUser>(_userManager);
                _userManager.Update(user);
                _uow.Context.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Опис: Методот врши барање на корисник од база
        // Влезни параметри: податочна вредност userName
        // Излезни параметри: Task од успешноста на пребараното
        public async Task<bool> FindUserInUsers(string userName)
        {
            var userExists = _userManager.Users.Any(x => x.UserName == userName);

            return userExists;
        }

        // Опис: Методот врши барање на ролја од база
        // Влезни параметри: податочна вредност userId
        // Излезни параметри: Task од успешноста на пребарано
        public async Task<List<string>> FindRoleAsync(string userId)
        {
            //var roles = _roleManager.Roles.Where(r => r.Name == "Admin" || r.Name == "SuperAdmin" || r.Name == "User");
            //var roleName =
            //    roles.Where(
            //        x => x.Id == x.Users.Where(y => y.UserId == userId).Select(y => y.RoleId).FirstOrDefault())
            //        .Select(x => x.Name)
            //        .FirstOrDefault();
            var roles = _roleManager.Roles;
            var userRolesIds = _userManager.FindById(userId).Roles.Select(x => x.RoleId).ToList();
            var userRolesNames = from x in roles
                                 join y in userRolesIds on x.Id equals y
                                 select x.Name;
            var temp = userRolesNames;
            return userRolesNames.ToList();
        }

        public string FindRole(string userId)
        {
            var roles = _roleManager.Roles.Where(r => r.Name == "Admin" || r.Name == "SuperAdmin" || r.Name == "User");
            var roleName =
                roles.Where(
                    x => x.Id == x.Users.Where(y => y.UserId == userId).Select(y => y.RoleId).FirstOrDefault())
                    .Select(x => x.Name)
                    .FirstOrDefault();

            return roleName;
        }

        // Опис: Методот врши барање на ролја од база
        // Влезни параметри: податочна вредност role
        // Излезни параметри: Task од успешноста на пребарано
        public async Task<bool> FindRoleInRoles(string role)
        {
            var roleExists = _roleManager.Roles.Any(x => x.Name == role);
            return roleExists;
        }

        // Опис: Методот врши вчитување на сите ролји од база
        // Влезни параметри: /
        // Излезни параметри: IQueryable<IdentityRole>
        public List<RoleViewModel> GetUserRoles(IPrincipal currentUser, List<string> userRoles)
        {
            var userRolesOutput = new List<RoleViewModel>();
            var roles = _roleManager.Roles.Where(r => r.Name == "Admin" || r.Name == "SuperAdmin" || r.Name == "User");
            foreach (var role in roles)
            {
                var isSelected = userRoles.Where(x => x == role.Id).FirstOrDefault();
                userRolesOutput.Add(new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsAvailableForSelecting = IsAvailable(currentUser, role.Name),
                    IsSelected = isSelected == null ? false : true
                });
            }
            return userRolesOutput;
        }
        // Опис: Методот врши вчитување на сите ролји од база
        // Влезни параметри: /
        // Излезни параметри: IQueryable<IdentityRole>
        public List<RoleViewModel> GetServiceRoles(List<string> userRoles)
        {
            var serviceRoles = new List<RoleViewModel>();
            var roles = _roleManager.Roles.Where(r => r.Name != "Admin" && r.Name != "SuperAdmin" && r.Name != "User");

            foreach (var role in roles)
            {
                var available = userRoles.Where(x => x == role.Id).FirstOrDefault();
                serviceRoles.Add(new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsAvailableForSelecting = available == null ? true : false,//ovaa da go trgneme
                    IsSelected = available == null ? true : false
                });
            }
            return serviceRoles;
        }
        public List<RoleViewModel> GetUserRolesRegister(IPrincipal loggedUser)
        {
            var roles = _roleManager.Roles
                  .Where(r => r.Name == "Admin" || r.Name == "SuperAdmin" || r.Name == "User")
                  .Select(x => x.Id).ToList();

            return GetUserRoles(loggedUser, roles);
        }
        public List<string> GetServiceRolesRegister()
        {
            var roles = _roleManager.Roles
                .Where(r => r.Name != "Admin" && r.Name != "SuperAdmin" && r.Name != "User").Select(x => x.Name).ToList();
            //.ToDictionary(x => x.Name,
            //               x => x.Id,
            //               StringComparer.OrdinalIgnoreCase);
            return roles;
        }

        // Опис: Методот враќа дали улогата може да биде менувана од страна на моментално логираниот корисник
        // Влезни параметри: IPrincipal currentUser, име на улога
        // Излезни параметри: bool
        private bool IsAvailable(IPrincipal currentUser, string role)
        {
            if (currentUser.IsInRole("SuperAdmin"))
            {
                return true;
            }
            if (currentUser.IsInRole("Admin"))
            {
                if (role == "SuperAdmin" || role == "Admin")
                {
                    return false;
                }
                return true;
            }
            return false;
        }


        // Опис: Методот врши креирање на корисник во база
        // Влезни параметри: ApplicationUser user
        // Излезни параметри: Task од успешноста на IdentityResult
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        // не се користи
        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        // Опис: Методот врши вчитување на сите корисници од база
        // Влезни параметри: /
        // Излезни параметри: PagedCollection<ApplicationUser> 
        public IQueryable<ApplicationUser> GetUsers()
        {
            return _userManager.Users;
        }
        // Опис: Методот врши вчитување на сите корисници од база
        // Влезни параметри: /
        // Излезни параметри: PagedCollection<ApplicationUser> 
        public ApplicationUser GetUser(string id)
        {
            return _userManager.Users.Where(x => x.Id == id).FirstOrDefault();
        }


        // RefreshToken & OWIN

        // Опис: Методот врши вчитување на клиент од база
        // Влезни параметри: податочна вредност clientId
        // Излезни параметри: Client 
        public Client FindClient(string clientId)
        {
            var client = _uow.Context.Clients.Find(clientId);

            return client;
        }

        // Опис: Методот врши додавање на Рефреш Токен во база
        // Влезни параметри: RefreshToken token
        // Излезни параметри: Task од успешноста на внесеното 
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {

            var existingToken = _uow.Context.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            try
            {
                _uow.Context.RefreshTokens.Add(token);
                var res = await _uow.Context.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        // Опис: Методот врши отстранување на Рефреш Токен од база
        // Влезни параметри: податочна вредност refreshTokenId
        // Излезни параметри: Task од успешноста на внесеното 
        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _uow.Context.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                try
                {
                    _uow.Context.RefreshTokens.Remove(refreshToken);
                    return await _uow.Context.SaveChangesAsync() > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            return false;
        }

        // Опис: Методот врши отстранување на Рефреш Токен од база
        // Влезни параметри: RefreshToken refreshToken
        // Излезни параметри: Task од успешноста на внесеното 
        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                _uow.Context.RefreshTokens.Remove(refreshToken);
                return await _uow.Context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Опис: Методот врши барање на Рефреш Токен од база
        // Влезни параметри: податочна вредност refreshTokenId
        // Излезни параметри: Task од успешноста на пронајдениот RefreshToken
        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _uow.Context.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }

        // Опис: Методот врши вчитување на сите Рефреш Токени од база
        // Влезни параметри: /
        // Излезни параметри: List<RefreshToken>
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _uow.Context.RefreshTokens.ToList();
        }

        public List<IdentityRoleModel> GetRoles()
        {
            var identityRoleList = new List<IdentityRoleModel>();
            var roles = _roleManager.Roles;
            foreach (var identityRole in roles)
            {
                identityRoleList.Add(new IdentityRoleModel
                {
                    RoleId = identityRole.Id,
                    RoleName = identityRole.Name
                });
            }
            return identityRoleList;
        }

        public void DeleteIdentityRole(string roleName)
        {
            if (_roleManager.RoleExists(roleName))
            {
                var getRole = _roleManager.FindByName(roleName);
                _roleManager.Delete(getRole);
            }
        }

        public void ChangePassword(ApplicationUser user)
        {
            try
            {
                _userManager.UserValidator = new CustomUserValidator<ApplicationUser>(_userManager);
                _userManager.Update(user);
                _uow.Context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetUserRoles()
        {
            var list = _roleManager.Roles.Where(x => x.Name == "Admin" || x.Name == "User" || x.Name == "SuperAdmin").Select(x => x.Name).ToList();

            return list;
        }
        public void Dispose()
        {
            _uow.Context.Dispose();
        }

    }
}
