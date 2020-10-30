using System;
using System.Collections.Generic;

partial class HierarchyModels
{
  public class SquareMeter
  {
    public string Type { get; set; }
    public double Area { get; set; }
  }

  public class Context
  {
    public string ApplicationCode { get; set; }
    public string ApplicationName { get; set; }
  }

  public class GeneralFilter
  {
    public string ClientCode { get; set; }
    public string CampusCode { get; set; }
    public string BuildingCode { get; set; }
    public string FloorCode { get; set; }
    public string TemplateType { get; set; }
    public string Status { get; set; }
    public string DiagramRef { get; set; }
    public string PlanCode { get; set; }
    public string Hash { get; set; }
    public string ApiKey { get; set; }
  }

  public class Data : GeneralFilter
  {
    public int PlanRevision { get; set; }
    public string Description { get; set; }
    public double UnitCost { get; set; }
    public string Currency { get; set; }
    public int Quantity { get; set; }
    public List<string> AdditionalPlans { get; set; }
    public List<SquareMeter> SquareMeters { get; set; }
  }



  public class CodeNameModel
  {
    public string Code { get; set; }
    public string Name { get; set; }
  }

  public class GenericViewModel : CodeNameModel
  {
    public string Address { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateUpdated { get; set; }
    public string Status { get; set; }
    public bool Chargeable { get; set; }
  }

  public class PartnerViewModel : CodeNameModel
  {
  }

  public class ClientViewModel : CodeNameModel
  {
    public string Address { get; set; }
    public string Status { get; set; }
    public bool Chargeable { get; set; }
    public List<CampusViewModel> Campuses { get; set; }
  }

  public class CampusViewModel : GenericViewModel
  {
    public string PlanCode { get; set; }
    public double? Longitude { get; set; }
    public double? Latitude { get; set; }

    public List<BuildingViewModel> Buildings { get; set; }
    public List<CampusViewModel> Campuses { get; set; }

    //        public PlanMetadata SitePlanMetadata { get; set; }
  }

  public class BuildingViewModel : GenericViewModel
  {
    public string CrossStreetAddress { get; set; }
    public List<FloorViewModel> Floors { get; set; }
  }

  public class FloorViewModel : GenericViewModel
  {
    public string PlanCode { get; set; }

    // public PlanMetadata PlanMetadata { get; set; }
  }

  
    public class LatLng {
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }

        public LatLng(double lng, double lat) {
          Longitude = lng;
          Latitude = lat;
        }
    }

}
