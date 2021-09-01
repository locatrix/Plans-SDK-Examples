using System;
using Newtonsoft.Json;

namespace esapi_building_footprints
{
    public class ViewTokenModels
    {
        public class CampusViewerTokenModel
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string PlanCode { get; set; }
            public string ClientCode { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateUpdated { get; set; }
            public string Status { get; set; }
            public ViewerToken ViewerTokens { get; set; }
        }

        public class BuildingInfoViewModel
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string CrossStreetAddress { get; set; }
            public string PostCode { get; set; }
            public double? Latitude { get; set; }
            public double? Longitude { get; set; }
            public string CampusCode { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateUpdated { get; set; }
            public string Status { get; set; }
            public bool Chargeable { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public ViewerToken ViewerTokens { get; set; }
        }

        public class FloorViewerTokenModel
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string PlanCode { get; set; }
            public string BuildingCode { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateUpdated { get; set; }
            public string Status { get; set; }
            public ViewerToken ViewerTokens { get; set; }
        }

        public class ViewerToken
        {
            public string AllAreas { get; set; }
            public DateTime Expiry { get; set; }
        }
    }
}