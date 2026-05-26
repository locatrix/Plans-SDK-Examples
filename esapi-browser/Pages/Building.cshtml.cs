using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using esapi_browser.Shared;
using esapi_browser.Server;

namespace esapi_browser.Pages
{
    public class BuildingModel : PageModel
    {
        private readonly EsapiSettings _settings;

        public string PartnerCode { get; set; }
        public string ClientCode { get; set; }
        public string CampusCode { get; set; }
        public string BuildingCode { get; set; }
        public HierarchyModels.FloorViewModel[] Floors { get; set; }
        public string ErrorMessage { get; set; }

        public BuildingModel(EsapiSettings settings)
        {
            _settings = settings;
        }

        public async Task OnGetAsync(string ptnr, string clnt, string camp, string bld)
        {
            PartnerCode = ptnr;
            ClientCode = clnt;
            CampusCode = camp;
            BuildingCode = bld;

            try
            {
                var token = await EsapiServerHelpers.GetBearerTokenEsapi(
                    _settings.ApplicationId,
                    _settings.ApplicationSecret,
                    _settings.ApiKey,
                    _settings.ApiSecret);

                var campusData = await EsapiServerHelpers.GetCampusList(token, PartnerCode, ClientCode);
                var campus = campusData.Campuses.FirstOrDefault(c => c.Code == CampusCode);
                if (campus != null)
                {
                    var building = campus.Buildings.FirstOrDefault(b => b.Code == BuildingCode);
                    if (building != null)
                    {
                        Floors = building.Floors.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error loading floors: {ex.Message}";
            }
        }
    }
}



