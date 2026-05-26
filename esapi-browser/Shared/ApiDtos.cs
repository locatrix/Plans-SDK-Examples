using System;

namespace esapi_browser.Shared
{
    // DTOs for API responses
    public class HierarchyResponse<T>
    {
        public T Data { get; set; }
    }

    public class ViewerTokenResponse
    {
        public string ViewerToken { get; set; }
    }
}

