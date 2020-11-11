using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace esapi_building_footprints
{
    internal class Program
    {
        // Plans SDK Public account
        private const string ApplicationId = "app_822gf6lf9cvrmja8kj6j22t8s"; 
        private const string ApplicationSecret = "kK7Qk42GsG5NlQIdzw4nuRHK0";
        private const string ApiKey = "65101207-60c6-2250-3ec1-c99f3eb20793";
        private const string ApiSecret = "tlEQfN4PWfoJa0jNg/unshwf6SesDsF3rknNtwLYO+E=";

        public const string EnterpriseServicesApiUrl = "https://api.locatrix.com/esapi/api/v1";
        public const string EmbedApiUrl = "https://api.locatrix.com/plans-embed/v1";
        public const string AuthenticationApiUrl = "https://auth.locatrix.com";

        public static string BearerToken;
        public static readonly int ExpiresIn = (int)System.TimeSpan.FromDays(1.0).TotalSeconds;

        private static void Main(string[] args)
        {

            EnumerateAllBuildings();
        }

        private static void EnumerateAllBuildings()
        {
            BearerToken = $"bearer {Helpers.GetBearerTokenEsapi(ApplicationId, ApplicationSecret, ApiKey, ApiSecret)}";
            var partnerList = Helpers.GetPartnerList(BearerToken);
            using (var writeFile = new StreamWriter($"./building-data.geojson.js"))
            {
                Console.Out.WriteLine($"Found {partnerList.Count} partners ... please wait");

                foreach (var partner in partnerList)
                {
                    BearerToken = $"bearer {Helpers.GetBearerTokenEsapi(ApplicationId, ApplicationSecret, ApiKey, ApiSecret)}";
                    Console.Out.WriteLine($"Partner: {partner.Name} {partner.Code}");

                    var clientsSummaryList = Helpers.GetClientsSummaryList(BearerToken, partner.Code);
                    foreach (var client in clientsSummaryList)
                    {
                        Console.Out.WriteLine($"  Client: {client.Name}");

                        var clientDetailViewModel = Helpers.GetClientDetails(BearerToken, partner.Code, client.Code);

                        foreach (var campus in clientDetailViewModel.Campuses
                                    .Where(i => i.Latitude != null) // guard should not be required for well drafted plans
                            )
                        {
                            Console.Out.WriteLine($"    Campus: {campus.Name} {campus.Latitude}");

                            foreach (var building in campus.Buildings)
                            {
                                Console.Out.WriteLine($"      Building: {building.Name} {building.Code}");

                                var outline = Helpers.GetBuildingOutline(BearerToken, partner.Code, building.Code);
                                writeFile.WriteLine($"buildings.push({outline});");

                                var pin = JsonPrettify(CreateDropPin(partner, client, campus, building));
                                writeFile.WriteLine($"buildings.push({pin});");
                            }
                        }
                        writeFile.Flush();
                    }
                }
            }
            
            Console.Out.WriteLine($"... complete.");

            Console.Out.WriteLine(@"Now open '.\building-outlines-example.html' in a modern browser to see all your buildings.");
        }

        private static string CreateDropPin(HierarchyModels.PartnerViewModel partner,
            HierarchyModels.ClientSummaryModel client, HierarchyModels.CampusViewModel campus,
            HierarchyModels.BuildingViewModel building)
        {
            var viewToken = Helpers.GetCampusViewerToken(Program.BearerToken, partner.Code, campus.Code);
            var embedApiUrlCampus = $"{EmbedApiUrl}/plan?layers=structure,equipment,zone&interactive=true&viewerToken=" + viewToken.ViewerTokens.AllAreas;

            var floorlinks = new List<string>();
            building.Floors.ForEach(fvm => floorlinks.Add(FloorlinkJsonBlob(fvm, partner.Code)));

            var dropPinGeoJson = @$"{{'type': 'Feature',
                                    'geometry': {{
                                        'type': 'Point',
                                        'coordinates': [{campus.Longitude}, {campus.Latitude}]
                                    }},
                                    'properties': {{
                                        'description': '{HttpUtility.JavaScriptStringEncode(building.Address)}',
                                        'partner' : '{HttpUtility.JavaScriptStringEncode(partner.Name)}',
                                        'client' : '{HttpUtility.JavaScriptStringEncode(client.Name)}',
                                        'campus' : '{HttpUtility.JavaScriptStringEncode(campus.Name)}',
                                        'building' : '{HttpUtility.JavaScriptStringEncode(building.Name)}',
                                        'siteplan': '{HttpUtility.JavaScriptStringEncode(embedApiUrlCampus)}',
                                        'floors': [
                                            {string.Join(",", floorlinks)}
                                        ],
                                    }}}}";
            return dropPinGeoJson;
        }

        private static string FloorlinkJsonBlob(HierarchyModels.FloorViewModel fvm, string partnerCode)
        {
            var floorViewToken = Helpers.GetFloorViewerToken(Program.BearerToken, partnerCode, fvm.Code);

            // See the documentation for layers and icons information
            // https://api.locatrix.com/docs/plans-static-api/generating-images/layer-names.html

            var embedApiFloorUrl = $"{EmbedApiUrl}/plan?layers=structure,zone&interactive=true&viewerToken={floorViewToken.ViewerTokens.AllAreas}";

            var link = $@"{{
                                    'name' : '{HttpUtility.JavaScriptStringEncode(fvm.Name)}',
                                    'link' : '{HttpUtility.JavaScriptStringEncode(embedApiFloorUrl)}',
                                }}";

            return link;
        }

        public static string JsonPrettify(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }
    }
}