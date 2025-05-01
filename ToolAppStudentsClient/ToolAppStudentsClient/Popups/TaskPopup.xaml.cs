using CommunityToolkit.Maui.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolAppStudentsClient.ViewModel;

namespace ToolAppStudentsClient.Popups
{
    public partial class TaskPopup:Popup
    {
        public TaskPopup(TaskPopupViewModel taskPopupViewModel) 
        {
            InitializeComponent();
            BindingContext=taskPopupViewModel;
        }
    }
}
