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