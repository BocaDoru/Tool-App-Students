using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ToolAppStudentsClient.DTO;
using ToolAppStudentsClient.Services;
using ToolAppStudentsClient.View;

namespace ToolAppStudentsClient.ViewModel
{
    public partial class RegisterViewModel : ObservableObject
    {
        [ObservableProperty]
        private RegisterDto registerDto;

        private readonly ClientServices _clientServices;
        public RegisterViewModel(ClientServices clientServices)
        {
            _clientServices = clientServices;
            registerDto = new();
        }
        [RelayCommand]
        private async Task Register()
        {
            await _clientServices.Register(registerDto);
        }
        [RelayCommand]
        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}

