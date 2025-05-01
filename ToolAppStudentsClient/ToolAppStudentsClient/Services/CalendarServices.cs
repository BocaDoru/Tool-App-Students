using System.Net.Http.Json;
using System.Text.Json;
using ToolAppStudentsClient.DTO;
using ToolAppStudentsClient.Models;
using ToolAppStudentsClient.Response;
using System.Net.Http.Headers;
using System.Net;
using ToolAppStudentsClient.Service;


namespace ToolAppStudentsClient.Services
{
    public class CalendarServices
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ClientServices _clientServices;
        public CalendarServices(IHttpClientFactory httpClientFactory, ClientServices clientServices)
        {
            this.httpClientFactory = httpClientFactory;
            _clientServices = clientServices;
        }
        public async Task<List<TaskModel>> GetWeekTasks()
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            string accessToken = await LoginResponse.GetAccessTokenFromSecureStorage();
            string refreshToken = await LoginResponse.GetRefreshTokenFromSecureStorage();
            ApiClient apiClient = new ApiClient(httpClientFactory, accessToken, refreshToken);
            var response = await apiClient.GetAsync<List<CalendarTaskResponse>>("/api/UserControllers/get_weektask");
            if (response is not null)
            {
                List<TaskModel> tasks = new List<TaskModel>();
                foreach (var item in response)
                {
                    tasks.Add(new TaskModel
                    {
                        Name = item.Title,
                        Day = item.Day,
                        Start = item.Start,
                        End = item.End,
                        Duration = item.Duration,
                        Notification = item.Notification.HasValue ? item.Notification.Value : TimeSpan.Zero,
                        Description = item.Description,
                        TaskColor = Color.FromRgb(item.Color[0], item.Color[1], item.Color[2])
                    });
                }
                await Shell.Current.DisplayAlert("Alert", "Getting tasks successfully", "OK");
                return tasks;
            }
            await Shell.Current.DisplayAlert("Alert", "Getting tasks failed", "OK");
            return null;
        }
        public async Task SetTask(TaskDto taskDto)
        {
            var httpClient = httpClientFactory.CreateClient("custom-httpclient");
            int? userId = 0;
            if (userId != null)
            {
                var result = await httpClient.PostAsJsonAsync($"/api/UserControllers/set_task?userId={userId}", taskDto);
                if (result.IsSuccessStatusCode)
                    await Shell.Current.DisplayAlert("Alert", "Setting task successfully", "OK");
                else
                    await Shell.Current.DisplayAlert("Alert", "Setting task error", "OK");
            }
        }
    }
}
