using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using esapi_browser.Server;

namespace esapi_browser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampusesController : ControllerBase
    {
        private readonly EsapiSettings _settings;

        public CampusesController(EsapiSettings settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public async Task<IActionResult> GetCampuses([FromQuery] string partnerCode, [FromQuery] string clientCode)
        {
            try
            {
                var token = await EsapiServerHelpers.GetBearerTokenEsapi(
                    _settings.ApplicationId,
                    _settings.ApplicationSecret,
                    _settings.ApiKey,
                    _settings.ApiSecret);

                var campuses = await EsapiServerHelpers.GetCampusList(token, partnerCode, clientCode);
                return Ok(campuses);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

