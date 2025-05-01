using System.Net.Http.Headers;
using System.Text.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Json;
using ToolAppStudentsClient.Response;
using ToolAppStudentsClient.View;

namespace ToolAppStudentsClient.Service
{
    public class ApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly HttpClient _httpClient;
        private string _accessToken;
        private string _refreshToken;

        public ApiClient(IHttpClientFactory httpClientFactory, string accessToken, string refreshToken)
        {
            this.httpClientFactory = httpClientFactory;
            _httpClient = httpClientFactory.CreateClient("custom-httpclient");
            _accessToken = accessToken;
            _refreshToken = refreshToken;
        }
        public async Task<TResponse> MakeAuthenticatedRequest<TRequest, TResponse>(HttpMethod method, string endpoint, TRequest requestBody = default)
        {
            try
            {
                return await MakeRequestWithRetry<TRequest, TResponse>(method, endpoint, requestBody);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return default;
            }
        }

        public async Task<TResponse> MakeRequestWithRetry<TRequest, TResponse>(HttpMethod method, string endpoint, TRequest requestBody = default)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            HttpResponseMessage response;
            if (requestBody != null)
            {
                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                switch (method.Method)
                {
                    case "POST":
                        response = await _httpClient.PostAsync(endpoint, content);
                        break;
                    case "PUT":
                        response = await _httpClient.PutAsync(endpoint, content);
                        break;
                    case "GET":
                        response = await _httpClient.GetAsync(endpoint);
                        break;
                    case "PATCH":
                        response = await _httpClient.PatchAsync(endpoint, content);
                        break;
                    case "DELETE":
                        response = await _httpClient.DeleteAsync(endpoint);
                        break;
                    default:
                        throw new ArgumentException($"Unsupported HTTP method:{method.Method}");
                }
            }
            else
            {
                response = await _httpClient.SendAsync(new HttpRequestMessage(method, endpoint));
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (await RefreshToken())
                {
                    return await MakeRequestWithRetry<TRequest, TResponse>(method, endpoint, requestBody);
                }
                else
                {
                    Console.WriteLine("Refresh token failed");
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                    return default;
                }
            }
            response.EnsureSuccessStatusCode();
            var responseJson=await response.Content.ReadFromJsonAsync<TResponse>();
            return responseJson;
        }

        public async Task<TResponse> GetAsync<TResponse>(string endpoint)
        {
            return await MakeAuthenticatedRequest<object, TResponse>(HttpMethod.Get, endpoint);
        }
        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest requestBody)
        {
            return await MakeAuthenticatedRequest<object, TResponse>(HttpMethod.Post, endpoint, requestBody);
        }
        public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest requestBody)
        {
            return await MakeAuthenticatedRequest<object, TResponse>(HttpMethod.Put, endpoint, requestBody);
        }

        public async Task<bool> RefreshToken()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null)
            {
                LoginResponse loginResponse = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!;
                var result = await _httpClient.PostAsJsonAsync("/api/UserControllers/refresh-token", loginResponse);
                if (result.IsSuccessStatusCode)
                {
                    var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                    _accessToken = response.AccessToken;
                    _refreshToken = response.RefreshToken;
                    var serializeResponse = JsonSerializer.Serialize(response);
                    SecureStorage.Default.Remove("Authentication");
                    await SecureStorage.Default.SetAsync("Authentication", serializeResponse);
                    return true;
                }
            }
            return false;
        }
    }

}
