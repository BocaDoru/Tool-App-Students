using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ToolAppStudentsClient.Response
{
    public class LoginResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? Expiration {  get; set; }
        public string? Username { get; set; }
        public static async Task<string> GetAccessTokenFromSecureStorage()
        {
            string jwtToken = null;
            try
            {
                var serializedResponse = await SecureStorage.Default.GetAsync("Authentication");
                if (serializedResponse is not null)
                {
                    jwtToken = JsonSerializer.Deserialize<LoginResponse>(serializedResponse)!.AccessToken!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return jwtToken;
        }
        public static async Task<string> GetRefreshTokenFromSecureStorage()
        {
            string responseToken = null;
            try
            {
                var serializedResponse = await SecureStorage.Default.GetAsync("Authentication");
                if (serializedResponse is not null)
                {
                    responseToken = JsonSerializer.Deserialize<LoginResponse>(serializedResponse)!.RefreshToken!;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return responseToken;
        }
    }
}
