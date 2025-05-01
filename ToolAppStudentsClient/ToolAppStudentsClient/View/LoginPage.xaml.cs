using ToolAppStudentsClient.ViewModel;

namespace ToolAppStudentsClient.View
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            BindingContext=loginViewModel;
        }
    }
}
