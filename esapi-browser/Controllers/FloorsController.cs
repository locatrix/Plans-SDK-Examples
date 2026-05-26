using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using esapi_browser.Server;

namespace esapi_browser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FloorsController : ControllerBase
    {
        private readonly EsapiSettings _settings;

        public FloorsController(EsapiSettings settings)
        {
            _settings = settings;
        }

        [HttpGet("{floorCode}/viewertoken")]
        public async Task<IActionResult> GetFloorViewerToken(string floorCode, [FromQuery] string partnerCode)
        {
            try
            {
                var token = await EsapiServerHelpers.GetBearerTokenEsapi(
                    _settings.ApplicationId,
                    _settings.ApplicationSecret,
                    _settings.ApiKey,
                    _settings.ApiSecret);

                var floorData = await EsapiServerHelpers.GetFloorViewerToken(token, partnerCode, floorCode);
                return Ok(new { viewerToken = floorData.ViewerTokens?.AllAreas });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

