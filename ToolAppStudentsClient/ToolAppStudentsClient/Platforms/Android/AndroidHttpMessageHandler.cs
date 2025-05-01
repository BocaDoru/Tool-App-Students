using System.Net.Security;
using Xamarin.Android.Net;

namespace ToolAppStudentsClient
{
    public class AndroidHttpMessageHandler : IPlatformHttpMessageHandler
    {
        public HttpMessageHandler CreateHttpMessageHandler() => new AndroidMessageHandler
        {
            ServerCertificateCustomValidationCallback = (HttpRequestMessage, certificate, chain, SslPolicyErrors) =>
            certificate?.Issuer == "CN=localhost" || SslPolicyErrors == SslPolicyErrors.None
        };

    }
}
