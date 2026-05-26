using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Json;

namespace esapi_browser.Server
{
    /// <summary>
    /// Server-side helpers for ESAPI calls. These use server credentials and should never be exposed to the client.
    /// </summary>
    static class EsapiServerHelpers
    {
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// Get bearer token using server-side credentials
        /// </summary>
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

            var response = await Client.RequestPasswordTokenAsync(passwordTokenRequest);
            if (response.IsError)
            {
                throw new Exception($"Failed to get bearer token: {response.Error}");
            }
            return response.AccessToken;
        }

        /// <summary>
        /// Get partner list with server-side bearer token
        /// </summary>
        public static async Task<HierarchyModels.PartnerViewModel[]> GetPartnerList(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.EnterpriseServicesApiUrl}/Partners");
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

            using var httpResponse = await Client.SendAsync(request);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get partners: {httpResponse.StatusCode}");
            }
            return await httpResponse.Content.ReadFromJsonAsync<HierarchyModels.PartnerViewModel[]>();
        }

        /// <summary>
        /// Get client list with server-side bearer token
        /// </summary>
        public static async Task<HierarchyModels.ClientViewModel[]> GetClientList(string token, string partnerCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.EnterpriseServicesApiUrl}/Partners/{partnerCode}/Clients");
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

            using var httpResponse = await Client.SendAsync(request);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get clients: {httpResponse.StatusCode}");
            }
            return await httpResponse.Content.ReadFromJsonAsync<HierarchyModels.ClientViewModel[]>();
        }

        /// <summary>
        /// Get campus list with server-side bearer token
        /// </summary>
        public static async Task<HierarchyModels.CampusViewModel> GetCampusList(string token, string partnerCode, string clientCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{Constants.EnterpriseServicesApiUrl}/Clients/{clientCode}");
            request.Headers.Add("X-CSS-Partner-Code", partnerCode);
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

            using var httpResponse = await Client.SendAsync(request);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get campuses: {httpResponse.StatusCode}");
            }
            return await httpResponse.Content.ReadFromJsonAsync<HierarchyModels.CampusViewModel>();
        }

        /// <summary>
        /// Get floor viewer token with server-side bearer token
        /// </summary>
        public static async Task<FloorViewerTokenModel> GetFloorViewerToken(string token, string partnerCode, string floorCode)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                $"{Constants.EnterpriseServicesApiUrl}/Floors/{floorCode}?partnerCode={partnerCode}&viewerTokenLifetime={Constants.TokenValidityMinutes}");

            request.Headers.Add("X-CSS-Partner-Code", partnerCode);
            request.Headers.Authorization = new AuthenticationHeaderValue("bearer", token);

            using var httpResponse = await Client.SendAsync(request);
            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get floor viewer token: {httpResponse.StatusCode}");
            }
            return await httpResponse.Content.ReadFromJsonAsync<FloorViewerTokenModel>();
        }

        /// <summary>
        /// Get campus viewer token with server-side bearer token
        /// </summary>
        public static CampusViewerTokenModel GetCampusViewerToken(string bearerToken, string partnerCode, string campusCode)
        {
            var restClient = new RestClient($"{Constants.EnterpriseServicesApiUrl}/Campuses/{campusCode}?partnerCode={partnerCode}&viewerTokenLifetime={Constants.TokenValidityMinutes}");
            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", bearerToken);
            var response = restClient.ExecuteAsync(request).Result;

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception($"Failed to get campus viewer token: {response.StatusCode}");
            }

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var obj = JsonConvert.DeserializeObject<CampusViewerTokenModel>(response.Content, settings);
            return obj;
        }
    }
}

