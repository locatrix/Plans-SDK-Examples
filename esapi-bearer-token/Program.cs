using System;
using System.Net.Http;
using Duende.IdentityModel.Client;

namespace esapi_bearer_token;

class Program
{
    static void Main(string[] args)
    {
        /*
         * To run this example, you need to have an application registered in our system with the following credentials:
         * - Application ID
         * - Application Secret
         * - API Key
         * - API Secret
         * 
         * You can obtain these credentials by contacting our support team or your account manager. See README.md.
         */

        const string ApplicationId = ""; // Your application ID
        const string ApplicationSecret = ""; // Your application secret
        const string ApiKey = ""; // Your API key
        const string ApiSecret = ""; // Your API secret
        
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
