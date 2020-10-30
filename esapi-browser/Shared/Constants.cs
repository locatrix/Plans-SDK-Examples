public static class Prefixes {
    public static readonly string Partner = "ptnr";
    public static readonly string Client = "clnt";
    public static readonly string Campus = "camp";
    public static readonly string Building = "bld";
    public static readonly string Floor = "flr";
}

public static class Constants {
        public static readonly string ApiKey = "65101207-60c6-2250-3ec1-c99f3eb20793"; // TODO: fill this in with your own API key
        public static readonly string ApiSecret = "tlEQfN4PWfoJa0jNg/unshwf6SesDsF3rknNtwLYO+E="; // TODO: fill this in with your own API secret
        
        public static readonly string ApplicationId = "app_822gf6lf9cvrmja8kj6j22t8s";
        public static readonly string ApplicationSecret = "kK7Qk42GsG5NlQIdzw4nuRHK0";

        public static readonly string EnterpriseServicesApiUrl = "https://api.locatrix.com/esapi/api/v1";
        public static readonly string EmbedApiUrl = "https://api.locatrix.com/plans-embed/v1";
        public static readonly string AuthenticationApiUrl = "https://auth.locatrix.com";

        public static readonly int TokenValidityMinutes = (int)System.TimeSpan.FromDays(1.0).TotalMinutes;
}