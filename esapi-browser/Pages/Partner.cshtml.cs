using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using esapi_browser.Shared;
using esapi_browser.Server;

namespace esapi_browser.Pages
{
    public class PartnerModel : PageModel
    {
        private readonly EsapiSettings _settings;

        public string PartnerCode { get; set; }
        public HierarchyModels.ClientViewModel[] Clients { get; set; }
        public string ErrorMessage { get; set; }

        public PartnerModel(EsapiSettings settings)
        {
            _settings = settings;
        }

        public async Task OnGetAsync(string ptnr)
        {
            PartnerCode = ptnr;

            try
            {
                var token = await EsapiServerHelpers.GetBearerTokenEsapi(
                    _settings.ApplicationId,
                    _settings.ApplicationSecret,
                    _settings.ApiKey,
                    _settings.ApiSecret);

                Clients = await EsapiServerHelpers.GetClientList(token, PartnerCode);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading clients: {ex.Message}";
            }
        }
    }
}


