using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using esapi_browser.Shared;
using esapi_browser.Server;

namespace esapi_browser.Pages
{
    public class ClientModel : PageModel
    {
        private readonly EsapiSettings _settings;

        public string PartnerCode { get; set; }
        public string ClientCode { get; set; }
        public HierarchyModels.CampusViewModel Campuses { get; set; }
        public string ErrorMessage { get; set; }

        public ClientModel(EsapiSettings settings)
        {
            _settings = settings;
        }

        public async Task OnGetAsync(string ptnr, string clnt)
        {
            PartnerCode = ptnr;
            ClientCode = clnt;

            try
            {
                var token = await EsapiServerHelpers.GetBearerTokenEsapi(
                    _settings.ApplicationId,
                    _settings.ApplicationSecret,
                    _settings.ApiKey,
                    _settings.ApiSecret);

                Campuses = await EsapiServerHelpers.GetCampusList(token, PartnerCode, ClientCode);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading campuses: {ex.Message}";
            }
        }
    }
}


