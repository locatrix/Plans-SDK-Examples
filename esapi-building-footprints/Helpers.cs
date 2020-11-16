using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json;
using RestSharp;

namespace esapi_building_footprints
{
    internal static class Helpers
    {
        public static string GetBearerTokenEsapi(string applicationId, string applicationSecret, string apiKey,
            string apiSecret)
        {
            var client = new HttpClient();
            var passwordTokenRequest = new PasswordTokenRequest
            {
                Address = $"{Program.AuthenticationApiUrl}/connect/token",

                ClientId = applicationId,
                ClientSecret = applicationSecret,

                Scope = "esapi-scope",

                UserName = apiKey,
                Password = apiSecret
            };

            var response = client.RequestPasswordTokenAsync(passwordTokenRequest).Result;
            return response.AccessToken;
        }

        public static List<HierarchyModels.PartnerViewModel> GetPartnerList(string bearerToken)
        {
            var client = new RestClient($"{Program.EnterpriseServicesApiUrl}/Partners");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", bearerToken);
            var response = client.Execute(request);

            var obj = GetResponseObject<List<HierarchyModels.PartnerViewModel>>(response);
            return obj;
        }

        public static List<HierarchyModels.ClientSummaryModel> GetClientsSummaryList(string bearerToken,
            string partnerCode)
        {
            var client = new RestClient($"{Program.EnterpriseServicesApiUrl}/Partners/{partnerCode}/Clients");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", bearerToken);
            var response = client.Execute(request);

            var obj = GetResponseObject<List<HierarchyModels.ClientSummaryModel>>(response);
            return obj;
        }

        public static HierarchyModels.ClientDetailViewModel GetClientDetails(string bearerToken, string partnerCode,
            string clientCode)
        {
            var client = new RestClient($"{Program.EnterpriseServicesApiUrl}/Clients/{clientCode}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", bearerToken);
            request.AddHeader("X-CSS-Partner-Code", partnerCode);
            var response = client.Execute(request);

            var obj = GetResponseObject<HierarchyModels.ClientDetailViewModel>(response);
            return obj;
        }

        public static ViewTokenModels.CampusViewerTokenModel GetCampusViewerToken(string bearerToken, string partnerCode, string campusCode)
        {
            var client = new RestClient($"{Program.EnterpriseServicesApiUrl}/Campuses/{campusCode}?expiry={Program.ExpiresIn}"); // seconds from now!
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", Program.BearerToken);
            request.AddHeader("X-CSS-Partner-Code", partnerCode);
            IRestResponse response = client.Execute(request);

            var obj = GetResponseObject<ViewTokenModels.CampusViewerTokenModel>(response);
            return obj;
        }

        public static ViewTokenModels.FloorViewerTokenModel GetFloorViewerToken(string bearerToken, string partnerCode, string floorCode)
        {
            var client = new RestClient($"{Program.EnterpriseServicesApiUrl}/Floors/{floorCode}?expiry={Program.ExpiresIn}"); // seconds from now!
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", Program.BearerToken);
            request.AddHeader("X-CSS-Partner-Code", partnerCode);
            IRestResponse response = client.Execute(request);

            var obj = GetResponseObject<ViewTokenModels.FloorViewerTokenModel>(response);
            return obj;
        }

        public static IRestResponse GetBuildingOutline(string bearerToken, string partnerCode, string buildingCode)
        {
            var client = new RestClient($"{Program.EnterpriseServicesApiUrl}/Buildings/{buildingCode}/Outlines");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", bearerToken);
            request.AddHeader("X-CSS-Partner-Code", partnerCode);
            var response = client.Execute(request);

            return response;
        }

        private static T GetResponseObject<T>(IRestResponse response) where T : new()
        {           
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Unexpected response from ESAPI: [{response.StatusCode} {response.StatusDescription}] {response.ErrorMessage}");

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            var obj = JsonConvert.DeserializeObject<T>(response.Content, settings);
            return obj;
        }
    }
}