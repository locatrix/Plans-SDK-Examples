using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using esapi_browser.Server;

namespace esapi_browser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        private readonly EsapiSettings _settings;

        public PartnersController(EsapiSettings settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public async Task<IActionResult> GetPartners()
        {
            try
            {
                var token = await EsapiServerHelpers.GetBearerTokenEsapi(
                    _settings.ApplicationId,
                    _settings.ApplicationSecret,
                    _settings.ApiKey,
                    _settings.ApiSecret);

                var partners = await EsapiServerHelpers.GetPartnerList(token);
                return Ok(partners);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

