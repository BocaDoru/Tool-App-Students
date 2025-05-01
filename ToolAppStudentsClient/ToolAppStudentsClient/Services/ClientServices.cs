using System.Net.Http.Json;
using System.Text.Json;
using ToolAppStudentsClient.DTO;
using ToolAppStudentsClient.Models;
using ToolAppStudentsClient.Response;
using System.Net.Http.Headers;
using System.Net;

namespace ToolAppStudentsClient.Services
{
    public class ClientServices
    {
        private readonly IHttpClientFactory httpClientFactory;
        public ClientServices(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task Register(RegisterDto registerDto)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/api/UserControllers/register", registerDto);
            if (result.IsSuccessStatusCode)
            {
                await Shell.Current.DisplayAlert("Alert", "successfully Register", "OK");
            }
            await Shell.Current.DisplayAlert("Alert", result.ReasonPhrase, "OK");
        }
        public async Task Login(LoginDto loginDto)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            var result = await httpClient.PostAsJsonAsync("/api/UserControllers/login", loginDto);
            var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
            if (response is not null)
            {
                var serializeResponse = JsonSerializer.Serialize(response);
                await SecureStorage.Default.SetAsync("Authentication", serializeResponse);
            }
        }
    }
}
