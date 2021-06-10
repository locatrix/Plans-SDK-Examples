using System;
using Newtonsoft.Json;


public class ViewerTokens
{
  public string AllAreas { get; set; }
  public DateTime Expiry { get; set; }
}

public class CampusViewerTokenModel
{
  public string Code { get; set; }
  public string Name { get; set; }
  public string PlanCode { get; set; }
  public string ClientCode { get; set; }
  public DateTime DateCreated { get; set; }
  public DateTime DateUpdated { get; set; }
  public string Status { get; set; }

  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public ViewerTokens ViewerTokens { get; set; }
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
  [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
  public ViewerTokens ViewerTokens { get; set; }
}