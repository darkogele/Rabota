using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Interop.CS.Models.Repository;
using Interop.CS.Models.RepositoryContracts;

namespace Interop.CS.Portal.API.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {

        private readonly IAuthRepository _authRepository;

        public RefreshTokensController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        // Опис: Методот го повикува GetAllRefreshTokens методот на АuthRepository модулот
        // Влезни параметри: /
        // Излезни параметри: НТТР статус и сите рефреш токени
        [Authorize(Users = "Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_authRepository.GetAllRefreshTokens());
        }

        // Опис: Методот го повикува RemoveRefreshToken методот на  МuthRepository модулот
        // Влезни параметри: идентификациски број за токен
        // Излезни параметри: НТТР статус
        //[Authorize(Users = "Admin")]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await _authRepository.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

    }
}
