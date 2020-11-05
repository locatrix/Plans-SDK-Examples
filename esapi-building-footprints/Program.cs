using System;
using System.Net.Http;
using IdentityModel.Client;

namespace esapi_building_footprints
{
    class Program
    {
        static void Main(string[] args)
        {

            /// Plans SDK Public account
            const string ApplicationId = "app_822gf6lf9cvrmja8kj6j22t8s";
            const string ApplicationSecret = "kK7Qk42GsG5NlQIdzw4nuRHK0";
            const string ApiKey = "65101207-60c6-2250-3ec1-c99f3eb20793";
            const string ApiSecret = "tlEQfN4PWfoJa0jNg/unshwf6SesDsF3rknNtwLYO+E=";

            var bearerToken = $"bearer {GetBearerTokenEsapi(ApplicationId, ApplicationSecret, ApiKey, ApiSecret)}";

            // EnumerateAllBuildings(bearerToken);
            Console.WriteLine("Test");

        }

        public static string GetBearerTokenEsapi(string applicationId, string applicationSecret, string apiKey, string apiSecret)
        {
            var client = new HttpClient();
            var ncswpf_passwordTokenRequest = new PasswordTokenRequest
            {
                Address = "https://auth.locatrix.com/connect/token",

                ClientId = applicationId,
                ClientSecret = applicationSecret,

                Scope = "crmadapterservice-api-scope",

                UserName = apiKey,
                Password = apiSecret
            };

            var response = client.RequestPasswordTokenAsync(ncswpf_passwordTokenRequest).Result;
            return response.AccessToken;
        }
    }
}
