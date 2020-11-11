using System;
using System.Collections.Generic;

namespace esapi_building_footprints
{
    internal class HierarchyModels
    {
        public class PartnerViewModel
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }

        public class ClientSummaryModel
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Status { get; set; }
            public bool Chargeable { get; set; }
        }

        public class ClientDetailViewModel
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Status { get; set; }
            public bool Chargeable { get; set; }
            public List<CampusViewModel> Campuses { get; set; }
        }

        public class GenericViewModel
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string Status { get; set; }
            public bool Chargeable { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateUpdated { get; set; }
        }

        public class CampusViewModel : GenericViewModel
        {
            public string PlanCode { get; set; }
            public double? Longitude { get; set; }
            public double? Latitude { get; set; }
            public List<BuildingViewModel> Buildings { get; set; }
        }

        public class BuildingViewModel : GenericViewModel
        {
            public string CrossStreetAddress { get; set; }
            public List<FloorViewModel> Floors { get; set; }
        }

        public class FloorViewModel : GenericViewModel
        {
            public string PlanCode { get; set; }
        }
    }
}