using ToolAppStudentsClient.Popups;
using ToolAppStudentsClient.View;

namespace ToolAppStudentsClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(WeekCalendar), typeof(WeekCalendar));
            Routing.RegisterRoute(nameof(TaskPopup), typeof(TaskPopup));
        }
    }
}
