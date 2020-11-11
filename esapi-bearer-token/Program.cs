using System;
using System.Net.Http;
using IdentityModel.Client;

namespace esapi_bearer_token
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

            var token = GetBearerTokenEsapi(ApplicationId, ApplicationSecret, ApiKey, ApiSecret);
            if (string.IsNullOrEmpty(token))
            {
                Console.Out.WriteLine("Incorrect credentials?");
                return;
            }

            var bearerToken = $"bearer {token}";

            Console.WriteLine(bearerToken);
            Console.WriteLine();
            Console.WriteLine("This token can be used in all calls to the Enterprise Services API (ESAPI) to access the Plans SDK Partnership.  It is valid for 1 hour from now.");
            Console.WriteLine("This token can be used to Authorize your session in the Swagger test site at https://api.locatrix.com/esapi/api/docs/index.html");
        }

        public static string GetBearerTokenEsapi(string applicationId, string applicationSecret, string apiKey, string apiSecret)
        {
            var client = new HttpClient();
            var passwordTokenRequest = new PasswordTokenRequest
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
    }
}
