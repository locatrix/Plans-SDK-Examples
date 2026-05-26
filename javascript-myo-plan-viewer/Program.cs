using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using IdentityModel.Client;
using Newtonsoft.Json;
using RestSharp;

namespace javaascript_icon_iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ApplicationId = ""; // your application id
            const string ApplicationSecret = ""; // your application secret
            const string ApiKey = ""; // your API key
            const string ApiSecret = ""; // your API secret

            var partnerCode = "ptnr_0djkhp1jlh316r3n48alwnzp9"; // SDK Partner
            var floorCode = "flr_x568tux35349qxw15o3v3dsfr"; // Block A

            var token = GetBearerTokenEsapi(ApplicationId, ApplicationSecret, ApiKey, ApiSecret);
            if (string.IsNullOrEmpty(token))
            {
                Console.Out.WriteLine("Incorrect credentials?");
                return;
            }

            var bearerToken = $"bearer {token}";

            var floorViewToken = GetFloorViewerToken(bearerToken, partnerCode, floorCode);

            Console.WriteLine();
            Console.WriteLine("This floor viewer token below between the dashes can be used in calls to the JavaScript SDK loadPlan method to access the selected floor and all it's areas & icons.  It is valid for 1 hour from now.");
            Console.WriteLine("This token can now be inserted in the line:");
            Console.WriteLine("    var allAreasToken = \"<your view token>\"");
            Console.WriteLine("in the file '.\\icon-iterator-and-plan-viewer.html'.");           
             Console.WriteLine();
            Console.WriteLine("---------------------------------");
            Console.WriteLine(floorViewToken.ViewerTokens.AllAreas);
            Console.WriteLine("---------------------------------");

        }

        public static string GetBearerTokenEsapi(string applicationId, string applicationSecret, string apiKey, string apiSecret)
        {
            var client = new HttpClient();
            var passwordTokenRequest = new IdentityModel.Client.PasswordTokenRequest
            {
                Address = "https://auth.locatrix.com/connect/token",

                ClientId = applicationId,
                ClientSecret = applicationSecret,

                Scope = "esapi-scope",

                UserName = apiKey,
                Password = apiSecret
            };

            var response = client.RequestPasswordTokenAsync(passwordTokenRequest).Result;
            return response.AccessToken;
        }

        public static FloorViewerTokenModel GetFloorViewerToken(string bearerToken, string partnerCode, string floorCode)
        {
            string EnterpriseServicesApiUrl = "https://api.locatrix.com/esapi/api/v1";

            var expiry = 3600 * 24 * 365 * 10;
            
            var client = new RestSharp.RestClient($"{EnterpriseServicesApiUrl}/Floors/{floorCode}?expiry={expiry}"); // seconds from now!
            client.Timeout = -1;
            var request = new RestSharp.RestRequest(RestSharp.Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", bearerToken);
            request.AddHeader("X-CSS-Partner-Code", partnerCode);
            RestSharp.IRestResponse response = client.Execute(request);

            var obj = GetResponseObject<FloorViewerTokenModel>(response);
            return obj;
        }
        private static T GetResponseObject<T>(RestSharp.IRestResponse response) where T : new()
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
