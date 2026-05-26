using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using esapi_browser.Shared;
using esapi_browser.Server;

namespace esapi_browser.Pages
{
    public class FloorModel : PageModel
    {
        private readonly EsapiSettings _settings;

        public string PartnerCode { get; set; }
        public string ClientCode { get; set; }
        public string CampusCode { get; set; }
        public string BuildingCode { get; set; }
        public string FloorCode { get; set; }
        public string EmbedURL { get; set; }
        public string ErrorMessage { get; set; }
        public bool CanRenderEmbedIframe { get; private set; }

        public FloorModel(EsapiSettings settings)
        {
            _settings = settings;
        }

        public async Task OnGetAsync(string ptnr, string clnt, string camp, string bld, string flr)
        {
            PartnerCode = ptnr;
            ClientCode = clnt;
            CampusCode = camp;
            BuildingCode = bld;
            FloorCode = flr;

            try
            {
                var token = await EsapiServerHelpers.GetBearerTokenEsapi(
                    _settings.ApplicationId,
                    _settings.ApplicationSecret,
                    _settings.ApiKey,
                    _settings.ApiSecret);

                var floor = await EsapiServerHelpers.GetFloorViewerToken(token, PartnerCode, FloorCode);
                if (floor?.ViewerTokens?.AllAreas != null)
                {
                    EmbedURL = $"{Constants.EmbedApiUrl}/plan?layers=structure,equipment,indicators,zone,sign&interactive=true&viewerToken=" + floor.ViewerTokens.AllAreas;
                }

                // The Locatrix embed endpoint rejects framing from localhost; only render iframe on known allowed hosts.
                var host = Request?.Host.Host ?? string.Empty;
                CanRenderEmbedIframe =
                    host.Equals("api.locatrix.com", StringComparison.OrdinalIgnoreCase) ||
                    host.Equals("plansstaticapidocs.z26.web.core.windows.net", StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading floor viewer: {ex.Message}";
            }
        }
    }
}
