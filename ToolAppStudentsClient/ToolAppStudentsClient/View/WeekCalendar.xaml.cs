using ToolAppStudentsClient.Models;
using ToolAppStudentsClient.ViewModel;

namespace ToolAppStudentsClient.View
{
    public partial class WeekCalendar : ContentPage
    {
        WeekCalendarViewModel viewmodel;
        public WeekCalendar(WeekCalendarViewModel weekCalendarViewModel)
        {
            InitializeComponent();
            BindingContext = weekCalendarViewModel;
            viewmodel=weekCalendarViewModel;
        }
    }
}
