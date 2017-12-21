using System.Threading.Tasks;
using System.Web.Http;
using Interop.CC.Models.Repository;
using Interop.CC.Models.RepositoryContracts;

namespace Interop.CC.Portal.API.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {

        private readonly IAuthRepository _authRepository;

        // Опис: Конструктор на RefreshTokensController модулот
        // Влезни параметри: IAuthRepository модел
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

