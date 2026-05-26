using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using esapi_browser.Shared;
using esapi_browser.Server;

namespace esapi_browser.Pages
{
    public class IndexModel : PageModel
    {
        private readonly EsapiSettings _settings;

        public HierarchyModels.PartnerViewModel[] Partners { get; set; }
        public string ErrorMessage { get; set; }

        public IndexModel(EsapiSettings settings)
        {
            _settings = settings;
        }

        public async Task OnGetAsync()
        {
            try
            {
                var token = await EsapiServerHelpers.GetBearerTokenEsapi(
                    _settings.ApplicationId,
                    _settings.ApplicationSecret,
                    _settings.ApiKey,
                    _settings.ApiSecret);

                Partners = await EsapiServerHelpers.GetPartnerList(token);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading partners: {ex.Message}";
            }
        }
    }
}


