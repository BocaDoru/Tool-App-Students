using ToolAppStudentsClient.ViewModel;

namespace ToolAppStudentsClient.View
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(RegisterViewModel registerViewModel)
        {
            InitializeComponent();
            BindingContext = registerViewModel;
        }
    }
}
