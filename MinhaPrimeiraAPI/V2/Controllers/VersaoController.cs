using Microsoft.AspNetCore.Mvc;

namespace MinhaPrimeiraAPI.V2.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/versao")]
    [ApiController]
    public class VersaoController : ControllerBase
    {
        /// <summary>
        /// Informa a versão da API.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string Valor()
        {
            return "Esta é a versão V2.";
        }
    }
}