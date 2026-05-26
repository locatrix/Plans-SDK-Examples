public static class Prefixes {
    public static readonly string Partner = "ptnr";
    public static readonly string Client = "clnt";
    public static readonly string Campus = "camp";
    public static readonly string Building = "bld";
    public static readonly string Floor = "flr";
}

public static class Constants {
        // ApiKey, ApiSecret, ApplicationId and ApplicationSecret have moved to
        // wwwroot/appsettings.json and are loaded via the EsapiSettings type (see Program.cs).

        public static readonly string EnterpriseServicesApiUrl = "https://api.locatrix.com/esapi/api/v1";
        public static readonly string EmbedApiUrl = "https://api.locatrix.com/plans-embed/v1";
        public static readonly string AuthenticationApiUrl = "https://auth.locatrix.com";

        public static readonly int TokenValidityMinutes = (int)System.TimeSpan.FromDays(1.0).TotalMinutes;
}