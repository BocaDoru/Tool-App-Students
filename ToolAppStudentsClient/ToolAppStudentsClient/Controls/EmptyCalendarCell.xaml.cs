using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolAppStudentsClient.Controls
{
    public partial class EmptyCalendarCell : ContentView
    {
        [RelayCommand]
        public void HandleEmptyButton()
        {
            Application.Current.MainPage.DisplayAlert("Button Clicked", $"You clicked an empty cell", "OK");
        }
        public EmptyCalendarCell()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
