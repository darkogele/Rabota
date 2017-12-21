using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Interop.CS.Models;
using Interop.CS.Models.DTO;
using Interop.CS.Models.Exceptions;
using Interop.CS.Models.Models;
using Interop.CS.Models.RepositoryContracts;
using Microsoft.AspNet.Identity;
using Interop.CS.Models.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Http;
using System.Net;
using Interop.CS.CrossCutting;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;

namespace Interop.CS.Portal.API.Controllers
{
    [RoutePrefix("api/Auth")]
    public class AuthController : ApiController
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPrincipal _loggedUser;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
            _loggedUser = HttpContext.Current.User;
        }

        // POST api/Account/Register
        //[Authorize(Roles = "Admin, SuperAdmin")]
        // Опис: Методот го повикува Register методот на AuthRepository модулот 
        // Влезни параметри: модел UserModelDTO
        // Излезни параметри: HТТР статус
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModelDTO userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await _authRepository.Register(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok();
        }

        // Опис: Методот го повикува GetAllUsers методот на AuthRepository модулот 
        // Влезни параметри: индекс за страна и број на записи по страна
        // Излезни параметри: Нумерирана листа од корисници
        [Authorize(Roles = "Admin, SuperAdmin")]
        public PagedCollection<ManagerUserViewModel> GetUsers(int pageIndex, int itemsPerPage, string sortDir, string sortCol)
        {
            return _authRepository.GetAllUsers(pageIndex, itemsPerPage, sortDir, sortCol, _loggedUser);
        }

        // Опис: Методот го повикува GetUsers методот на AuthRepository модулот 
        // Влезни параметри: идентификациски број за корисник
        // Излезни параметри: НТТР статус, корисничко име, ролја
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public IHttpActionResult GetUserRoles(string id)
        {
            var userDb = string.IsNullOrEmpty(id) ? _authRepository.GetUsers().FirstOrDefault(x => x.UserName == User.Identity.Name) : _authRepository.GetUsers().FirstOrDefault(x => x.Id == id);

            if (userDb == null)
                throw new Exception("Не постои таков корисник");

            return Ok(new { userDb.UserName, userDb.Email, Role = userDb.Roles.Select(x => x.RoleId), id = id });

        }
        
        public IHttpActionResult GetUserRolesForCurrentUser()
        {
            var currentUserId =
                    _authRepository.GetUsers()
                        .FirstOrDefault(x => x.UserName == _loggedUser.Identity.Name)
                        .Id;
            var userDb = string.IsNullOrEmpty(currentUserId) ? _authRepository.GetUsers().FirstOrDefault(x => x.UserName == User.Identity.Name) : _authRepository.GetUsers().FirstOrDefault(x => x.Id == currentUserId);

            if (userDb == null)
                throw new Exception("Не постои таков корисник");

            return Ok(new { userDb.UserName, userDb.Email, Role = userDb.Roles.Select(x => x.RoleId), id = currentUserId });

        }
       
        // Опис: Методот го повикува GetRoles методот на AuthRepository модулот 
        // Влезни параметри: /
        // Излезни параметри: НТТР статус, сите ролји
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            var roles = _authRepository.GetRolesForCurrentUser(User).ToList();

            return Ok(roles.Select(x => new { x.Id, x.Name, x.IsAvailableForSelecting }));
        }

        // Опис: Методот го повикува UpdateUser методот на AuthRepository модулот 
        // Влезни параметри: кориснички идентификациски број, стара идентификациски број за ролја, нов идентификациски број за ролја, корисничко име, лозинка, потврда за лозинка 
        // Излезни параметри: НТТР статус
        [Authorize(Roles = "Admin, SuperAdmin")]
        [HttpGet]
        public ApplicationUser UpdateUser(string userid, string email, string roleOldId, string roleNewId, string userName, string oldpassword, string password, string confirmPassword)
        {
            var userDb = string.IsNullOrEmpty(userid) ? _authRepository.GetUsers().FirstOrDefault(x => x.UserName == User.Identity.Name) : _authRepository.GetUsers().FirstOrDefault(x => x.Id == userid);

            var loggedUser =
                _authRepository.GetUsers().FirstOrDefault(x => x.UserName == _loggedUser.Identity.Name);

            if (userDb == null)
                throw new Exception("Не постои таков корисник");

            if (password != null && confirmPassword == null || password == null && confirmPassword != null)
            {
                throw new Exception("За промена на лозинка потребно е да се внесат: лозинка и потврда за лозинка");
            }
            if (loggedUser == null)
            {
                throw new Exception("Не постои најавен корисник.");
            }
            if (password != null && confirmPassword != null)
            {
                if (password.Length < 6)
                {
                    throw new Exception("Лозинката мора да содржи најмалку 6 карактери");
                }

                else if (password != confirmPassword)
        {
                    throw new Exception("Лозинките не се совпаѓаат");
                }
                var _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new InteropContext()));
                if (!_loggedUser.IsInRole("SuperAdmin"))
                {
                    if (oldpassword == null)
                    {
                        throw new Exception("За промена на лозинка потребно е да се внесе старата лозинка!");
                    }
                    if (_userManager.PasswordHasher.VerifyHashedPassword(userDb.PasswordHash, oldpassword) == PasswordVerificationResult.Failed)
                    {
                        throw new Exception("Старата лозинка е грешна");
                    }
                }
                var hashPass = _userManager.PasswordHasher.HashPassword(password);
                userDb.PasswordHash = hashPass;
            }

            // Implementirano vo Repository
            //var role = await _authRepository.FindRoleInRoles(roleOldId);
            //if (role)
            //    throw new Exception("Не постои таква улога");

            userDb.Roles.Clear();

            userDb.Roles.Add(new IdentityUserRole { RoleId = roleNewId, UserId = userid });  //First(x => x.RoleId == roleOldId).RoleId = roleNewId;
            userDb.UserName = userName;
            userDb.Email = email;

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

        [HttpPost]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = new PhotoMultipartFormDataStreamProvider(AppSettings.Get<string>("UploadCertPath"));
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            var certExtension = Path.GetExtension(result.FileData.First().LocalFileName);

            if (certExtension == ".cer")
            {
                //var originalFileName = GetDeserializedFileName(result.FileData.First());

                try
                {
                    var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);
                    var cert = new X509Certificate2();

                    cert = new X509Certificate2(uploadedFileInfo.FullName);
                    //StringBuilder builder = new StringBuilder();
                    //builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
                    //var stringBuilder = builder.ToString();
                    //StringBuilder builder = new StringBuilder();
                    //builder.AppendLine(Convert.ToBase64String(cert.Export(X509ContentType.Cert), Base64FormattingOptions.InsertLineBreaks));
                    //var stringBuilder = builder.ToString();
                    //RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)cert.PublicKey.Key;
                    var publicKey = cert.GetPublicKey();
                    //var p = cert.PublicKey;
                    //var publicKey = rsa.ToXmlString(false);
                    //return this.Request.CreateResponse(HttpStatusCode.OK, cert.PublicKey);
                     return Request.CreateResponse(HttpStatusCode.OK, Convert.ToBase64String(publicKey));
                }
                catch (CryptographicException ex)
                {

                    throw new InvalidPublicKeyForCertificate(ex.Message);
                }
            }
            else
            {
                throw new InvalidCertException(certExtension);
            }

        }
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
    }
}
