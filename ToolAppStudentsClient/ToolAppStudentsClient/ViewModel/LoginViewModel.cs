using System.ComponentModel;
using System.Net.Http.Json;
using System.Net.Http;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToolAppStudentsClient.DTO;
using ToolAppStudentsClient.Services;
using ToolAppStudentsClient.View;
using System.Text.Json;
using ToolAppStudentsClient.Response;
using System;

namespace ToolAppStudentsClient.ViewModel
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private LoginDto _loginDto;
        [ObservableProperty]
        private string _username;
        [ObservableProperty]
        private bool _isAuthenticated;

        private readonly ClientServices _clientServices;

        public LoginViewModel(ClientServices clientServices)
        {
            _clientServices = clientServices;
            _loginDto = new();
            _isAuthenticated = false;
            GetUsernameFromSecuredStorage();
        }
        [RelayCommand]
        private async Task Login()
        {
            await _clientServices.Login(_loginDto);
            GetUsernameFromSecuredStorage();
            if (!string.IsNullOrEmpty(_username))
                await Shell.Current.GoToAsync(nameof(WeekCalendar));
        }
        [RelayCommand]
        private async Task GoToRegisterPage()
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }
        private async void GetUsernameFromSecuredStorage()
        {
            if (!string.IsNullOrEmpty(_username))
            {
                _isAuthenticated = true;
                return;
            }
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null)
            {
                LoginResponse loginResponse = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!;
                _username = loginResponse.Username;
                _isAuthenticated=true;
                await Shell.Current.GoToAsync(nameof(WeekCalendar));
                return;
            }
        }
    }
}
