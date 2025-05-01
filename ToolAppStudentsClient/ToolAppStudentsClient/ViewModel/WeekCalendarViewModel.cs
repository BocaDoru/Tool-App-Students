using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Views;
using System.Timers;
using ToolAppStudentsClient.ColorsTheme;
using ToolAppStudentsClient.Models;
using ToolAppStudentsClient.Popups;
using CommunityToolkit.Maui.Core;
using System.Diagnostics;
using CommunityToolkit.Maui.Core.Extensions;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using ToolAppStudentsClient.Services;
using ToolAppStudentsClient.DTO;
using ToolAppStudentsClient.Controls;

namespace ToolAppStudentsClient.ViewModel
{
    public partial class WeekCalendarViewModel : ObservableObject
    {
        private readonly CalendarServices _calendarServices;

        [ObservableProperty]
        private ObservableCollection<TaskCalendarCell> _weekGridTasks;
        /*[ObservableProperty]
        private ObservableCollection<GridCell> _weekGridEmpty;
        [ObservableProperty]
        private ObservableCollection<GridCell> _weekGridDays;

        private DateTime[] _dates=new DateTime[7];
        private DateTime _currentDay;

        [ObservableProperty]
        private RowDefinition _rowDef;
        [ObservableProperty]
        private ColumnDefinition _colDef;

        private System.Timers.Timer _timer;*/

        [ObservableProperty]
        private DateOnly[] _dates = new DateOnly[7];
        [ObservableProperty]
        private string[] _weekDays;
        [ObservableProperty]
        private DateOnly _focusDate;

        public WeekCalendarViewModel(CalendarServices calendarServices)
        {
            _weekGridTasks = new ObservableCollection<TaskCalendarCell>();
            _calendarServices = calendarServices;
            _focusDate = DateOnly.FromDateTime(DateTime.Now);
            for (int i = 0; i < 7; i++)
            {
                _dates[i] = _focusDate.AddDays(-(int)_focusDate.DayOfWeek + i);
            }
            _weekDays = new string[] { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT" };
            /*_rowDef = new RowDefinition();
            _colDef = new ColumnDefinition();
            _weekGridTasks = new ObservableCollection<TaskCell>();
            _weekGridEmpty = new ObservableCollection<GridCell>();
            _weekGridDays = new ObservableCollection<GridCell>();
            _currentDay = DateTime.Now;
            
            GenerateGridData();
            _timer = new System.Timers.Timer(60000); // 60 seconds
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();*/
        }
        [RelayCommand]
        public async Task HandleLogoutButton()
        {
            SecureStorage.Default.Remove("Authentication");
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        private async void AddButton()
        {
            TaskPopupViewModel taskPopupViewModel = new TaskPopupViewModel();
            var result=await Shell.Current.ShowPopupAsync(new TaskPopup(taskPopupViewModel));
            if (result is TaskModel task)
            {
                /*await _calendarServices.SetTask(new TaskDto
                {
                    Name = task.Name,
                    Day = task.Day,
                    Start = task.Start,
                    End = task.End,
                    Duration = task.Duration,
                    Notification = task.Notification,
                    Description = task.Description,
                    TaskColor = Convert.ToBase64String(new byte[3]
                    {
                        task.TaskColor.GetByteRed(),
                        task.TaskColor.GetByteGreen(),
                        task.TaskColor.GetByteBlue()
                    }),
                });*/
                AddTask(task);
            }
            OnPropertyChanged(nameof(WeekGridTasks));
        }
        public void AddTask(TaskModel task)
        {
            DateOnly day = DateOnly.FromDateTime(task.Day);
            TimeOnly start = TimeOnly.FromTimeSpan(task.Start);
            TimeOnly end = TimeOnly.FromTimeSpan(task.End >= TimeSpan.FromDays(1) ? TimeSpan.Zero : task.End);
            TaskCalendarCell taskCalendarCell = new TaskCalendarCell
            {
                Day = day,
                StartHour=start,
                EndHour=end,
                TaskColor=task.TaskColor,
                Title=task.Name,
                Description=task.Description
            };
            WeekGridTasks.Add(taskCalendarCell);
            OnPropertyChanged(nameof(WeekGridTasks));
        }
        [RelayCommand]
        private void NextButton()
        {
            DateOnly[] newDates = new DateOnly[Dates.Length];
            FocusDate = FocusDate.AddDays(Dates.Length);
            for (int i = -(int)FocusDate.DayOfWeek; i < 7 - (int)FocusDate.DayOfWeek; i++)
            {
                newDates[i + (int)FocusDate.DayOfWeek] = FocusDate.AddDays(i);
            }
            Dates = newDates;
            OnPropertyChanged(nameof(Dates));
            OnPropertyChanged(nameof(FocusDate));
        }
        [RelayCommand]
        private void BackButton()
        {
            DateOnly[] newDates = new DateOnly[Dates.Length];
            FocusDate = FocusDate.AddDays(-Dates.Length);
            for (int i = -(int)FocusDate.DayOfWeek; i < 7 - (int)FocusDate.DayOfWeek; i++)
            {
                newDates[i + (int)FocusDate.DayOfWeek] = FocusDate.AddDays(i);
            }
            Dates = newDates;
            OnPropertyChanged(nameof(Dates));
            OnPropertyChanged(nameof(FocusDate));
        }
    }
}
