using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolAppStudentsClient.ColorsTheme;
using ToolAppStudentsClient.Models;

namespace ToolAppStudentsClient.ViewModel
{
    public partial class TaskPopupViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<TimeSpan> _timeListItems;
        [ObservableProperty]
        private TaskModel _taskDto;
        [ObservableProperty]
        private TaskColorButton[] _taskColorButtons;
        public TaskPopupViewModel()
        {
            _taskDto = new TaskModel
            {
                Day = DateTime.Now,
                Start = new TimeSpan(DateTime.Now.Hour,0,0),
                End = new TimeSpan(DateTime.Now.Hour+1,0,0),
                TaskColor=DarkBlueTheme.PictonBlue
            };
            _timeListItems = new ObservableCollection<TimeSpan>();
            TimeSpan time = new TimeSpan(0, 15, 0);
            for (int i = 0; i < 95; i++)
            {
                _timeListItems.Add(time);
                time = time.Add(new TimeSpan(0, 15, 0));
            }
            _taskColorButtons = new TaskColorButton[6];
            _taskColorButtons[0] = new TaskColorButton
            {
                Color = DarkBlueTheme.PictonBlue,
                IsVisible=true
            };
            _taskColorButtons[1] = new TaskColorButton
            {
                Color = DarkBlueTheme.YellowGreen,
                IsVisible = false
            };
            _taskColorButtons[2] = new TaskColorButton
            {
                Color = DarkBlueTheme.OrangePeel,
                IsVisible = false
            };
            _taskColorButtons[3] = new TaskColorButton
            {
                Color = DarkBlueTheme.Cerise,
                IsVisible = false
            };
            _taskColorButtons[4] = new TaskColorButton
            {
                Color = DarkBlueTheme.Grape,
                IsVisible = false
            };
            _taskColorButtons[5] = new TaskColorButton
            {
                Color = DarkBlueTheme.Moonstone,
                IsVisible = false
            };
        }
        [RelayCommand]
        public void ColorButtonClicked(TaskColorButton taskColorButton)
        {
            TaskDto.TaskColor = taskColorButton.Color;
            for (int i = 0; i < 6; i++)
            {
                _taskColorButtons[i].IsVisible = !_taskColorButtons[i].IsVisible;
            }
            taskColorButton.IsVisible = true;
            OnPropertyChanged(nameof(TaskColorButtons));
        }
        [RelayCommand]
        public void NotificationButtonClicked(TimeSpan timeSpan)
        {
            TaskDto.Notification = timeSpan;
        }
        [RelayCommand]
        public async Task CancelTask(object obj)
        {
            Popup popup = obj as Popup;
            await popup.CloseAsync(null);
        }

        [RelayCommand]
        public async Task SaveTask(object obj)
        {
            Popup popup = obj as Popup;
            TaskDto.Duration=TaskDto.End-TaskDto.Start;
            await popup.CloseAsync((object)TaskDto);
        }
    }
}
