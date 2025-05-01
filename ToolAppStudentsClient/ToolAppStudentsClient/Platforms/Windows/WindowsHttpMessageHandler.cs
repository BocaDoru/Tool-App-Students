namespace ToolAppStudentsClient
{
    public class WindowsHttpMessageHandler : IPlatformHttpMessageHandler
    {
        public HttpMessageHandler CreateHttpMessageHandler()
        {
            return new HttpClientHandler();
        }

    }
}
