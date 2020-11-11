using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using IdentityModel.Client;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Json;
using System.Threading.Tasks;

static class Helpers
{
    private static HttpClient client = new HttpClient();
    public static async Task<HierarchyModels.PartnerViewModel[]> GetPartnerList(string token)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.EnterpriseServicesApiUrl}/Partners");

        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

        using var httpResponse = await client.SendAsync(request);
        return await httpResponse.Content.ReadFromJsonAsync<HierarchyModels.PartnerViewModel[]>();
    }

    public static async Task<HierarchyModels.ClientViewModel[]> GetClientList(string token, string partnerCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.EnterpriseServicesApiUrl}/Partners/{partnerCode}/Clients");

        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

        using var httpResponse = await client.SendAsync(request);
        return await httpResponse.Content.ReadFromJsonAsync<HierarchyModels.ClientViewModel[]>();
    }

    public static async Task<HierarchyModels.CampusViewModel> GetCampusList(string token, string partnerCode, string clientCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.EnterpriseServicesApiUrl}/Clients/{clientCode}");

        request.Headers.Add("X-CSS-Partner-Code", partnerCode);
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

        using var httpResponse = await client.SendAsync(request);
        return await httpResponse.Content.ReadFromJsonAsync<HierarchyModels.CampusViewModel>();
    }

    private static T GetResponseObject<T>(IRestResponse response) where T : new()
    {
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new System.Exception($"Could not get events from ESAPI: {response.StatusCode}");

        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };
        var obj = JsonConvert.DeserializeObject<T>(response.Content, settings);
        return obj;
    }

    public static async Task<string> GetBearerTokenEsapi(string clientId, string clientSecret, string username, string password)
    {
        var passwordTokenRequest = new PasswordTokenRequest
        {
            Address = $"{Constants.AuthenticationApiUrl}/connect/token",

            ClientId = clientId,
            ClientSecret = clientSecret,
            Scope = "esapi-scope",

            UserName = username,
            Password = password
        };

        var response = await client.RequestPasswordTokenAsync(passwordTokenRequest);
        return response.AccessToken;
    }

    public static CampusViewerTokenModel GetCampusViewerToken(string bearerToken, string partnerCode, string campusCode)
    {
        var client = new RestClient($"{Constants.EnterpriseServicesApiUrl}/Campuses/{campusCode}?partnerCode={partnerCode}&viewerTokenLifetime={Constants.TokenValidityMinutes}"); // minutes from now!
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", bearerToken);
        IRestResponse response = client.Execute(request);

        var obj = GetResponseObject<CampusViewerTokenModel>(response);
        return obj;
    }

    public static async Task<FloorViewerTokenModel> GetFloorViewerToken(string token, string partnerCode, string floorCode)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.EnterpriseServicesApiUrl}/Floors/{floorCode}?partnerCode={partnerCode}&viewerTokenLifetime={Constants.TokenValidityMinutes}");

        request.Headers.Add("X-CSS-Partner-Code", partnerCode);
        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

        using var httpResponse = await client.SendAsync(request);
        return await httpResponse.Content.ReadFromJsonAsync<FloorViewerTokenModel>();
    }
}