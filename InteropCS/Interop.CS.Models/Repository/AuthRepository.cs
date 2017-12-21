using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Security.Principal;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Threading.Tasks;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Interop.CS.Models.UoW;
using System.Linq.Dynamic;

namespace Interop.CS.Models.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IUnitOfWork _uow;
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AuthRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new InteropContext()));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new InteropContext()));
        }

        // ASP.NET Identity

        //Опис: 
        //Влезни параметри: објект од класата UserModelDTO
        public async Task<IdentityResult> Register(UserModelDTO userModel)
        {
#if DEBUG
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Username,
                PublicKey = "testKey",
            };
#else
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Username,
                PublicKey = userModel.PublicKey,
            };
#endif
            var result = await _userManager.CreateAsync(user, userModel.Password);
            return result;
        }

        //Опис: Методот регистрира нов корисник
        //Влезни параметри: објект од класата UserModelDTO
        public void RegisterUser(UserModelDTO userModel)
        {
#if DEBUG
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Username,
                Email = userModel.Email,
                PublicKey = "testKey",
            };
#else
            ApplicationUser user = new ApplicationUser
            {
                UserName = userModel.Username,
                Email = userModel.Email,
                PublicKey = userModel.PublicKey,
            };
#endif
            try
            {
                _userManager.UserValidator = new CustomUserValidator<ApplicationUser>(_userManager);
                _userManager.Create(user, userModel.Password);
                _userManager.AddToRole(user.Id, userModel.UserRole);
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

        //Опис: Методот брише корисник
        //Влезни параметри: објект од класата ApplicationUser
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
        //Опис: Методот пронаоѓа одреден корисник
        //Влезни параметри: корисничко име, лозинка
        //Излезни параметри: Корисник (објект од класата ApplicationUser)
        public async Task<ApplicationUser> FindUser(string userName, string password)
        {
            ApplicationUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        private bool CanBeModified(ApplicationUser user, IPrincipal loggedInUser, List<IdentityRole> roles)
        {
            return HasCurrentUserRightsToModify(user, loggedInUser, roles);
        }

        private bool HasCurrentUserRightsToModify(ApplicationUser user, IPrincipal loggedInUser, List<IdentityRole> roles)
        {
            var superAdmin = roles.FirstOrDefault(x => x.Name == "SuperAdmin");
            var admin = roles.FirstOrDefault(x => x.Name == "Admin");
            if (superAdmin == null || admin == null)
            {
                throw new ObjectNotFoundException();
            }
            if (loggedInUser.IsInRole("Admin") && (user.Roles.Any(x => x.RoleId == admin.Id) || user.Roles.Any(x => x.RoleId == superAdmin.Id)))
            {
                return false;
            }
            if (user.Roles.Any(x => x.RoleId == superAdmin.Id))
            {
                return true;
            }
            return true;
        }

        // Опис: Методот врши вчитување на сите ролји од база кои моменталниот корисник може да ги менува
        // Влезни параметри: IPrincipal currentUser
        // Излезни параметри: List<RoleViewModel>
        public List<RoleViewModel> GetRolesForCurrentUser(IPrincipal currentUser)
        {
            var rolesForUser = new List<RoleViewModel>();
            var roles = GetRoles();
            foreach (var role in roles)
            {
                rolesForUser.Add(new RoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    IsAvailableForSelecting = IsAvailable(currentUser, role.Name)
                });
            }
            return rolesForUser;
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

        // Опис: Методот врши вчитување на сите корисници од база
        // Влезни параметри: податочна вредност pageIndex, itemsPerPage, IIdentity loggedInUser
        // Излезни параметри: PagedCollection<ManagerUserViewModel> 
        public PagedCollection<ManagerUserViewModel> GetAllUsers(int pageIndex, int itemsPerPage, string sortDir, string sortCol, IPrincipal loggedInUser)
        {
            var usersDb = GetUsers().ToList();

            var rolesDb = GetRoles();
            var userList = new List<ManagerUserViewModel>();
            foreach (var user in usersDb)
            {
                if (user.UserName != loggedInUser.Identity.Name)
                {
                    var userRolesId = user.Roles.Select(m => m.RoleId).ToList();
                    var model = new ManagerUserViewModel
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Email = user.Email,
                        Roles = rolesDb.Where(r => userRolesId.Contains(r.Id)).Select(x => x.Name).ToList(),
                        CanBeModified = CanBeModified(user, loggedInUser, rolesDb.ToList())
                    };
                    userList.Add(model);
                }
            }
            IQueryable<ManagerUserViewModel> usersPaged = userList.AsQueryable();

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

            var totalSize = GetUsers().Count();
            if (totalSize > 0)
            {
                totalSize = totalSize - 1;
            }
            return new PagedCollection<ManagerUserViewModel>(pageIndex, itemsPerPage, totalSize, pagedItems.ToList());
        }

        //Опис: Методот ги ажурира податоци за корисникот (влезниот параметар)
        //Влезни параметри: објект од класата ApplicationUser
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

        //Опис: Методот пронаоѓа одреден корисник според влезниот параметар
        //Влезни параметри: Корисничко име
        //Излезни параметри: Корисник
        public async Task<bool> FindUserInUsers(string userName)
        {
            var userExists = _userManager.Users.Any(x => x.UserName == userName);

            return userExists;
        }

        //Опис: Методот пронаоѓа одредена ролја
        //Влезни параметри: Id на корисник
        //Излезни параметри: роља 
        public async Task<string> FindRoleAsync(string userId)
        {
            var roleName =
                _roleManager.Roles.Where(
                    x => x.Id == x.Users.Where(y => y.UserId == userId).Select(y => y.RoleId).FirstOrDefault())
                    .Select(x => x.Name)
                    .FirstOrDefault();

            return roleName;
        }

        public string FindRole(string userId)
        {
            var roleName =
                _roleManager.Roles.Where(
                    x => x.Id == x.Users.Where(y => y.UserId == userId).Select(y => y.RoleId).FirstOrDefault())
                    .Select(x => x.Name)
                    .FirstOrDefault();

            return roleName;
        }
        //Опис: Методот проверува дали постои одредена ролја, според влезниот параметар
        //Влезни параметри: роља
        //Излезни параметри: Дали постои таа роља
        public async Task<bool> FindRoleInRoles(string role)
        {
            var roleExists = _roleManager.Roles.Any(x => x.Name == role);
            return roleExists;
        }

        //Опис: Методот ги вчитува сите ролји
        //Излезни параметри: Листа од сите ролји
        public IQueryable<IdentityRole> GetRoles()
        {
            var roles = _roleManager.Roles;
            return roles;
        }

        //Опис: Методот креира нов корисник
        //Влезни параметри: објект од класата ApplicationUser
        //Излезни параметри:
        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user);

            return result;
        }

        //Опис: Методот врши логирање 
        //Влезни параметри: Id на корисник, објект од класата UserLoginInfo
        //Излезни параметри:
        public async Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(userId, login);

            return result;
        }

        //Опис: Методот ги пронаоѓа сите корисници
        //Излезни параметри: Листа од сите Корисници
        public IQueryable<ApplicationUser> GetUsers()
        {
            return _userManager.Users;
        }

        // RefreshToken & OWIN

        //Опис:
        //Влезни параметри: Id на клиент
        //Излезни параметри: објект од класата Client
        public Client FindClient(string clientId)
        {
            var client = _uow.Context.Clients.Find(clientId);
            return client;
        }

        //Опис: Методот додава нов токен на освежување
        //Влезни параметри: објект од класата RefreshToken
        //Излезни параметри: 
        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            //var totalMilliseconds = (long)(DateTime.Now - new DateTime(2010, 1, 1)).TotalMilliseconds;
            //token.ClientId = token.ClientId + totalMilliseconds;

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

        //Опис: Методот брише токен на освежување според влезниот параметар
        //Влезни параметри: Id за токен на освежување

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

        //Опис: Методот пронаоѓа одреден токен на освежување
        //Влезни параметри: Id за токен на освежување
        //Излезни параметри: објект од класата RefreshToken
        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _uow.Context.RefreshTokens.FindAsync(refreshTokenId);

            return refreshToken;
        }
        //Опис: Методот ги пронаоѓа сите токени на освежување
        //Влезни параметри: Id за токен на освежување
        //Излезни параметри: Листа од токени на 
        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _uow.Context.RefreshTokens.ToList();
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

        public void Dispose()
        {
            _uow.Context.Dispose();
        }

    }
}
