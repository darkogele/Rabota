using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using Interop.CC.CrossCutting;
using Interop.CC.Models;
using Interop.CC.Models.DTO;
using Interop.CC.Models.Exceptions;
using Interop.CC.Models.Helper;
using Interop.CC.Models.RepositoryContracts;
using Microsoft.AspNet.Identity;
using System.Web.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Newtonsoft.Json;
using Interop.CC.Models.Models;
using System.Collections.Generic;

namespace Interop.CC.Portal.API.Controllers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPrincipal _loggedUser;

        // Опис: Конструктор на AuthController модулот 
        // Влезни параметри: модел IAuthRepository
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _loggedUser = HttpContext.Current.User;
        }

        // Опис: Методот го повикува GetAllUsers методот на AuthRepository модулот 
        // Влезни параметри: индекс за страна, број на записи по страна, насока за сортирање, колона за сортирање
        // Излезни параметри: Нумерирана листа од корисници
        [Authorize(Roles = "Admin, SuperAdmin")]
        public PagedCollection<UserViewModel> GetUsers(int pageIndex, int itemsPerPage, string sortDir = "", string sortCol = "", string userName = "", string userRole = "")
        {
            return _authRepository.GetAllUsers(pageIndex, itemsPerPage, sortDir, sortCol, userName, userRole, _loggedUser);
        }
       
        // Опис: Методот го повикува UpdateUser методот на AuthRepository модулот 
        // Влезни параметри: кориснички идентификациски број, стара идентификациски број за ролја, нов идентификациски број за ролја, корисничко име, лозинка, потврда за лозинка 
        // Излезни параметри: НТТР статус
        [Authorize(Roles = "User, Admin, SuperAdmin")]
        [HttpGet]
        public ManagerUserViewModel GetUserDetails(string userId)
        {
            if(userId == "own")
            {
                userId = _authRepository.GetUsers().FirstOrDefault(x => x.UserName == _loggedUser.Identity.Name).Id;
            }
            ManagerUserViewModel output = new ManagerUserViewModel();

            var userDb = _authRepository.GetUser(userId);
            if (userDb != null)
            {
                output.UserId = userId;
                output.UserName = userDb.UserName;
                output.Email = userDb.Email;
                output.UserRoles = _authRepository.GetUserRoles(_loggedUser, userDb.Roles.Select(x => x.RoleId).ToList());
                output.ServiceRoles = _authRepository.GetServiceRoles(userDb.Roles.Select(x => x.RoleId).ToList());
            }
            else
            {
                throw new Exception("Не постои таков корисник!");
            }

            return output;
        }
        // Опис: Методот го повикува UpdateUser методот на AuthRepository модулот 
        // Влезни параметри: кориснички идентификациски број, стара идентификациски број за ролја, нов идентификациски број за ролја, корисничко име, лозинка, потврда за лозинка 
        // Излезни параметри: НТТР статус
        [Authorize(Roles = "User, Admin, SuperAdmin")]
        [HttpPost]
        public ApplicationUser UpdateUser(UpdateUserViewModel updatedUser)
        {

            var userDb = string.IsNullOrEmpty(updatedUser.UserId) ? _authRepository.GetUsers().FirstOrDefault(x => x.UserName == User.Identity.Name) : _authRepository.GetUsers().FirstOrDefault(x => x.Id == updatedUser.UserId);
            
            var loggedUser =
                _authRepository.GetUsers().FirstOrDefault(x => x.UserName == _loggedUser.Identity.Name);

            if (userDb == null)
                throw new Exception("Не постои таков корисник");
           
            if ((updatedUser.Password != null && updatedUser.Password != "" && updatedUser.ConfirmPassword == null) || (updatedUser.Password == null && updatedUser.ConfirmPassword != null && updatedUser.ConfirmPassword != ""))
            {
                throw new Exception("За промена на лозинка потребно е да се внесат: лозинка и потврда за лозинка");
            }
            if (loggedUser == null)
            {
                throw new Exception("Не постои најавен корисник.");
            }
            if (updatedUser.Password != null  && updatedUser.ConfirmPassword != null )
            {
                if (updatedUser.Password.Length < 6)
                {
                    throw new Exception("Лозинката мора да содржи најмалку 6 карактери");
                }

                else if (updatedUser.Password != updatedUser.ConfirmPassword)
                {
                    throw new Exception("Лозинките не се совпаѓаат");
                }

                var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new InteropContext()));

                if (!_loggedUser.IsInRole("SuperAdmin"))
                {
                    if (updatedUser.Oldpassword == null)
                    {
                        throw new Exception("За промена на лозинка потребно е да се внесе старата лозинка!");
                    }
                    if (_userManager.PasswordHasher.VerifyHashedPassword(userDb.PasswordHash, updatedUser.Oldpassword) == PasswordVerificationResult.Failed)
                    {
                        throw new Exception("Старата лозинка е грешна");
                    }
                }
                var hashPass = _userManager.PasswordHasher.HashPassword(updatedUser.Password);
                userDb.PasswordHash = hashPass;
            }
            if (!_loggedUser.IsInRole("User"))
            {
                
                var userRolesDb = _authRepository.GetUserRoles(_loggedUser, userDb.Roles.Select(x => x.RoleId).ToList());
                var serviceRolesDb = _authRepository.GetServiceRoles(userDb.Roles.Select(x => x.RoleId).ToList());
                bool found = false;
                foreach (var roleDb in userDb.Roles)
                {
                    if (roleDb.RoleId == updatedUser.UserRole)
                        found = true;
                }
                if(!found)
                {
                    var oldId = userRolesDb.Where(x => x.IsSelected).FirstOrDefault().Id;
                    var oldRole = userDb.Roles.Where(x => x.RoleId == oldId).FirstOrDefault();
                    _authRepository.DeleteRole(userDb, oldRole);
                    userDb.Roles.Add(new IdentityUserRole { RoleId = updatedUser.UserRole, UserId = updatedUser.UserId });
                }
                var oldSelectedServiceRoles = serviceRolesDb.Where(x => !x.IsAvailableForSelecting).Select(x => x.Id);

                var intersect = updatedUser.SelectedServiceRoles1.Intersect(oldSelectedServiceRoles);
                var toDelete = oldSelectedServiceRoles.Except(intersect);
                var toAdd = updatedUser.SelectedServiceRoles1.Except(intersect);

                foreach (var toDel in toDelete)
                {
                    var del = userDb.Roles.Where(x => x.RoleId == toDel).FirstOrDefault();
                    _authRepository.DeleteRole(userDb, del);
                }
                foreach (var toAd in toAdd)
                {
                    userDb.Roles.Add(new IdentityUserRole { RoleId = toAd, UserId = updatedUser.UserId });
                }
            }
            userDb.UserName = updatedUser.UserName;
            userDb.Email = updatedUser.Email;

            try
            {
                _authRepository.UpdateUser(userDb, loggedUser);
                return loggedUser;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        public void ChangePassword(ChangePasswordModelDTO changePasswordData)
        {
            if (changePasswordData.Password.Length < 6)
            {
                throw new Exception("Лозинката мора да содржи најмалку 6 карактери");
            }
            if (changePasswordData.Password != changePasswordData.ConfirmPassword)
            {
                throw new Exception("Лозинките не се совпаѓаат");
            }
            if ((!string.IsNullOrEmpty(changePasswordData.Password) && changePasswordData.ConfirmPassword == null) || (changePasswordData.Password == null && !string.IsNullOrEmpty(changePasswordData.ConfirmPassword)))
            {
                throw new Exception("За промена на лозинка потребно е да се внесат: Лозинка и Потврда за лозинка");
            }

            var userDb = _authRepository.GetUsers().FirstOrDefault(x => x.UserName == changePasswordData.Username);
            if (userDb == null)
                throw new Exception("Не постои таков корисник");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new InteropContext()));

            var hashPassword = userManager.PasswordHasher.HashPassword(changePasswordData.Password);
            userDb.PasswordHash = hashPassword;

            try
            {
                _authRepository.ChangePassword(userDb);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        // Опис: Методот го повикува DeleteUser методот на AuthRepository модулот 
        // Влезни параметри: кориснички идентификациски број 
        // Излезни параметри: /
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public void DeleteUser(string userid)
        {

            var userDb = _authRepository.GetUsers().FirstOrDefault(x => x.Id == userid);
            var loggedUser =
                _authRepository.GetUsers().FirstOrDefault(x => x.UserName == _loggedUser.Identity.Name);
            if (userDb == null)
                throw new Exception("Не постои таков корисник");

            if (userDb.UserName == User.Identity.Name)
            {
                throw new Exception("Не е дозволено бришење на податоците.");
            }

            if (loggedUser == null)
            {
                throw new Exception("Не постои најавен корисник.");
            }

            try
            {
                _authRepository.DeleteUser(userDb, loggedUser);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        // Опис: Методот го повикува RegisterUser методот на AuthRepository модулот 
        // Влезни параметри: модел UserModelDTO
        // Излезни параметри: модел UserModelDTO
        [Route("RegisterUser")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<UserModelDTO> RegisterUser(UserModelDTO userModel)
        {
            if (userModel.Password.Length < 6)
            {
                throw new Exception("Лозинката мора да содржи најмалку 6 карактери");
            }

            else if (userModel.Password != userModel.ConfirmPassword)
            {
                throw new Exception("Лозинките не се совпаѓаат");
            }

            else if (userModel.UserRole.Length > 0)
            {
                var roleExists = await _authRepository.FindRoleInRoles(userModel.UserRole);
                if (!roleExists)
                {
                    throw new Exception("Внесовте непостоечка ролја");
                }
            }

            var user = await _authRepository.FindUserInUsers(userModel.Username);
            if (!user)
            {
                try
                {
                    _authRepository.RegisterUser(userModel);
                    return userModel;
                }
                catch (DuplicateUserException ex)
                {
                    throw new HttpException(ex.Message);
                }
            }
            else
            {
                throw new DuplicateUserException(userModel);
            }

        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public List<RoleViewModel> GetUserRoles()
        {
            return _authRepository.GetUserRolesRegister(_loggedUser);
        }
        private HttpResponseMessage SetResponseMessage(string message)
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK);
            resp.Content = new StringContent(message, System.Text.Encoding.UTF8, "text/plain");
            return resp;
        }

        [HttpPost]
        public HttpResponseMessage CreateAllUserRoles()
        {
            var allUsers = _authRepository.GetUsers().ToList();
            var loggedUser = _authRepository.GetUsers().FirstOrDefault(x => x.UserName == _loggedUser.Identity.Name);
            string usernameStr = string.Empty;
            if (allUsers.Count > 0)
            {
                foreach (var user in allUsers)
                {
                    var userRolesNames = _authRepository.FindRoleAsync(user.Id);

                    var userServiceRoles = userRolesNames.Result.Where(x => x != "Admin" && x != "SuperAdmin" && x != "User");

                    var identityRoles = _authRepository.GetRoles();

                    if (userServiceRoles.Count() > 0)
                    {
                        foreach (var roleName in userServiceRoles)
                        {
                            var role = identityRoles.FirstOrDefault(x => x.RoleName == roleName);
                            if (role != null)
                                user.Roles.Remove(new IdentityUserRole { UserId = user.Id, RoleId = role.RoleId });
                        }
                    }
                    else
                    {
                        if (identityRoles.Count > 0)
                        {
                            var identityRolesForServices = identityRoles.Where(x => x.RoleName != "Admin" && x.RoleName != "SuperAdmin" && x.RoleName != "User");
                            foreach (var identityRoleForService in identityRolesForServices)
                            {
                                user.Roles.Add(new IdentityUserRole { RoleId = identityRoleForService.RoleId, UserId = user.Id });
                            }
                        }
                    }
                    try
                    {
                        _authRepository.UpdateUser(user, loggedUser);
                        usernameStr += user.UserName + ", ";
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                if (!string.IsNullOrEmpty(usernameStr))
                {
                    return SetResponseMessage("Успешно искреирани улоги за корисници: " + usernameStr);
                }
            }

            return SetResponseMessage("Не постојат корисници за кои би се искреирале улоги.");
        }
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public List<string> GetServiceRoles()
        {
            return _authRepository.GetServiceRolesRegister();
        }
        // Опис: Методот го повикува GetCertPublicKey методот на AuthRepository модулот
        // Влезни параметри: /
        // Излезни параметри: јавен клуч сертификат
        [HttpPost]
        public async Task<HttpResponseMessage> GetCertPublicKey()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new PhotoMultipartFormDataStreamProvider(AppSettings.Get<string>("UploadCertPath"));
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
            var cert = new X509Certificate2();

            try
            {
                cert = new X509Certificate2(uploadedFileInfo.FullName);
                //StringBuilder builder = new StringBuilder();
                //builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
                //var stringBuilder = builder.ToString();
                //RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;
                var publicKey = cert.GetPublicKey();
                //var publicKey = rsa.ToXmlString(false);
                return Request.CreateResponse(HttpStatusCode.OK, Convert.ToBase64String(publicKey));
            }
            catch (CryptographicException ex)
            {

                throw new InvalidCertificate(ex.Message);
            }

        }

        // Опис: Методот врши десеријализација на податоци
        // Влезни параметри: модел од MultipartFileData
        // Излезни параметри: десеријализиран (конвертиран) податочен тип 
        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        // Опис: Методот врши екстракција на име на фајл
        // Влезни параметри: модел од MultipartFileData
        // Излезни параметри: податочен тип за име на фајл
        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        // Опис: Методот врши колекција на серверски грешки
        // Влезни параметри: модел IdentityResult 
        // Излезни параметри: листа од грешки 
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
        public List<string> GetUserRolesList(){ 
            var rolesList = _authRepository.GetUserRoles();

            return rolesList;
        }

    }
}
