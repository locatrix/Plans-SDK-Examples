using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using esapi_browser.Server;

namespace esapi_browser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly EsapiSettings _settings;

        public ClientsController(EsapiSettings settings)
        {
            _settings = settings;
        }

        [HttpGet("{partnerCode}")]
        public async Task<IActionResult> GetClients(string partnerCode)
        {
            try
            {
                var token = await EsapiServerHelpers.GetBearerTokenEsapi(
                    _settings.ApplicationId,
                    _settings.ApplicationSecret,
                    _settings.ApiKey,
                    _settings.ApiSecret);

                var clients = await EsapiServerHelpers.GetClientList(token, partnerCode);
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

