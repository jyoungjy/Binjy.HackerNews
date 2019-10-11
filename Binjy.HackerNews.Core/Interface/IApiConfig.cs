using System;

namespace Binjy.HackerNews.Core.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IApiConfig
    {
        string ApiVersion { get; set; }
        string BaseUri { get; set; }
        bool IsSecure { get; set; }
        Uri AsIndexEndpointUrl(string endpoint);
        Uri AsItemEndpointUrl(int id);
    }
}