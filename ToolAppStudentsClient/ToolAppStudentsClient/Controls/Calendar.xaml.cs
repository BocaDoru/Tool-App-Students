using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ToolAppStudentsClient.ColorsTheme;
using ToolAppStudentsClient.Converter;

namespace ToolAppStudentsClient.Controls
{
    public partial class Calendar : ContentView
    {
        public static readonly BindableProperty NumberOfDaysProperty = BindableProperty.Create(
            nameof(NumberOfDays), typeof(int), typeof(Calendar), 0, BindingMode.TwoWay,propertyChanged: NumberOfDaysPropertyChanged);
        public static readonly BindableProperty NumberOfVisibleRowsProperty = BindableProperty.Create(
            nameof(NumberOfVisibleRows), typeof(int), typeof(Calendar),0, BindingMode.TwoWay,propertyChanged:NumberOfVisibleRowsPropertyChanged);
        public static readonly BindableProperty WeekDaysProperty = BindableProperty.Create(
            nameof(WeekDays), typeof(string[]), typeof(Calendar), null, BindingMode.TwoWay, propertyChanged:WeekDaysPropertyChanged);
        public static readonly BindableProperty DatesProperty = BindableProperty.Create(
            nameof(Dates), typeof(DateOnly[]), typeof(Calendar), null, BindingMode.TwoWay, propertyChanged:DatesPropertyChanged);
        public static readonly BindableProperty FocusDateProperty = BindableProperty.Create(
            nameof(FocusDate), typeof(DateOnly), typeof(Calendar), null, BindingMode.TwoWay,propertyChanged:FocusDatePropertyChanged);
        public static readonly BindableProperty MonthProperty = BindableProperty.Create(
            nameof(Month), typeof(string), typeof(Calendar), null, BindingMode.TwoWay);
        /*public static readonly BindableProperty TaskListProperty=BindableProperty.Create(
            nameof(TaskList),typeof(List<TaskCalendarCell>),typeof(Calendar),null, BindingMode.TwoWay);*/
        public static readonly BindableProperty TaskItemsSourceProperty=BindableProperty.Create(
            nameof(TaskItemsSource),typeof(ObservableCollection<TaskCalendarCell>),typeof(Calendar),null, BindingMode.TwoWay,propertyChanged:TaskItemsSourcePropertyChanged);

        private DateTime _previousDay;
        private System.Timers.Timer _timer;

        [TypeConverter(typeof(Int32Converter))]
        public int NumberOfDays
        {
            get => (int)GetValue(NumberOfDaysProperty);
            set => SetValue(NumberOfDaysProperty, value);
        }
        [TypeConverter(typeof(Int32Converter))]
        public int NumberOfVisibleRows
        {
            get => (int)GetValue(NumberOfVisibleRowsProperty);
            set => SetValue(NumberOfVisibleRowsProperty, value);
        }
        [TypeConverter(typeof(StringArrayConverter))]
        public string[] WeekDays
        {
            get => (string[])GetValue(WeekDaysProperty);
            set => SetValue(WeekDaysProperty, value);
        }
        [TypeConverter(typeof(DateOnlyConverter))]
        public DateOnly[] Dates
        {
            get => (DateOnly[])GetValue(DatesProperty);
            set => SetValue(DatesProperty, value);
        }
        [TypeConverter(typeof(DateOnlyConverter))]
        public DateOnly FocusDate
        {
            get => (DateOnly)GetValue(FocusDateProperty);
            set => SetValue(FocusDateProperty, value);
        }
        public string Month
        {
            get => (string)GetValue(MonthProperty);
            set => SetValue(MonthProperty, value);
        }
        public ObservableCollection<TaskCalendarCell> TaskItemsSource
        {
            get => (ObservableCollection<TaskCalendarCell>)GetValue(TaskItemsSourceProperty);
            set => SetValue(TaskItemsSourceProperty, value);
        }
        private Page? _page;

        public static void NumberOfDaysPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar)
            {
                calendar.UpdateColumnWidth();
                if (oldValue is int oldValueInt && newValue is int newValueInt)
                {
                    if (oldValueInt < newValueInt)
                        for (int i = oldValueInt + oldValueInt == 0 ? 0 : 1; i < newValueInt; i++)
                        {
                            for (int j = 0; j < 24; j++)
                                calendar.calendarGridBodyEmpty.Add(new EmptyCalendarCell(), i, j);
                        }
                    else
                        for (int i = newValueInt + newValueInt == 0 ? 0 : 1; i < oldValueInt; i++)
                        {
                            calendar.calendarGridBodyEmpty.ToList().RemoveAll(v => calendar.calendarGridBodyEmpty.GetColumn(v) == i);
                        }
                }
            }
        }

        public static void NumberOfVisibleRowsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar)
            {
                calendar.UpdateRowHeight();
            }
        }

        private static Page? FindParentPage(Element element)
        {
            if (element == null)
                return null;
            if(element is Page page)
                return page;
            else
                return FindParentPage(element.Parent);
        }
        private static void UpdateGridRows(Grid grid, int rows, double rowHeight)
        {
            grid.RowDefinitions.Clear();
            for (int i = 0; i < rows; i++)
                grid.RowDefinitions.Add(new RowDefinition { Height = rowHeight });
        }
        private void UpdateRowHeight()
        {
            if (_page is null)
            {
                _page = FindParentPage(this);
                if (_page is null)
                    return;
            }

            double rowHeight = _page.Height / NumberOfVisibleRows;
            UpdateGridRows(calendarGridHeadButtons, 2, rowHeight);
            UpdateGridRows(calendarGridHead,2,rowHeight);
            UpdateGridRows(hourGrid,24,rowHeight);
            UpdateGridRows(calendarGridBodyEmpty,24,rowHeight);
            UpdateGridRows(calendarGridBody,24,rowHeight);
        }
        private static void UpdateGridColumns(Grid grid, int col, double colWidth)
        {
            grid.ColumnDefinitions.Clear();
            for (int i = 0; i < col; i++)
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = colWidth });
        }
        public void UpdateColumnWidth()
        {
            if (_page is null)
            {
                _page = FindParentPage(this);
                if (_page is null)
                    return;
            }

            double columnWidth = (_page.Width - 100) / NumberOfDays;
            UpdateGridColumns(calendarGridHead, NumberOfDays, columnWidth);
            UpdateGridColumns(calendarGridBodyEmpty, NumberOfDays, columnWidth);
            UpdateGridColumns(calendarGridBody, NumberOfDays, columnWidth);
        }
        public static void WeekDaysPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar)
            {
                calendar.calendarGridHead.Children.Where(v => calendar.calendarGridHead.GetRow(v) == 0).ToList().Clear();

                for (int i = 0; i < calendar.NumberOfDays; i++)
                    calendar.calendarGridHead.Add(new TextCalendarCell
                    {
                        Text = calendar.WeekDays[i],
                        CellColor = DarkBlueTheme.EerieBlack,
                        TextColor=Colors.White
                    }, i, 0);
            }
        }
        public static void DatesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar)
            {
                calendar.calendarGridHead.Children.Where(v => calendar.calendarGridHead.GetRow(v) == 1).ToList().Clear();

                for (int i = 0; i < calendar.NumberOfDays; i++)
                    calendar.calendarGridHead.Add(new DateCalendarCell
                    {
                        Date = calendar.Dates[i]
                    }, i, 1);
            }
        }
        public static void FocusDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Calendar calendar)
            {
                DateTime dateTime = DateTime.Now;
                calendar.Month = calendar.FocusDate.AddDays(-(int)calendar.FocusDate.DayOfWeek).ToString("MMMM/yyyy");
                calendar.monthText.Text = calendar.Month;
            }
        }
        public static void TaskItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is Calendar calendar)
            {
                if(oldValue is INotifyCollectionChanged oldCollection)
                {
                    oldCollection.CollectionChanged -= calendar.TaskItemsSourceCollectionChanged;
                }
                if(newValue is INotifyCollectionChanged newCollection)
                {
                    newCollection.CollectionChanged += calendar.TaskItemsSourceCollectionChanged;
                }
                calendar.UpdateGridBody(newValue as ObservableCollection<TaskCalendarCell>);
            }
        }
        private void TaskItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems is not null)
                    {
                        foreach (TaskCalendarCell newItem in e.NewItems)
                        {
                            AddItemToGridBody(newItem);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if(e.OldItems is not null)
                    {
                        foreach(TaskCalendarCell oldItem in e.OldItems)
                        {
                            RemoveItemFromGridBody(oldItem);
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    UpdateGridBody(TaskItemsSource);
                    break;
            }
        }
        private void AddItemToGridBody(TaskCalendarCell newItem)
        {
            calendarGridBody.Children.Add(newItem);
        }
        private void RemoveItemFromGridBody(TaskCalendarCell oldItem)
        {
            calendarGridBody.Children.Remove(oldItem);
        }
        private void UpdateGridBody(ObservableCollection<TaskCalendarCell> newItemsSource)
        {
            calendarGridBody.Children.Clear();
            if(newItemsSource is not null)
            {
                foreach (var item in newItemsSource)
                {
                    AddItemToGridBody(item);
                }
            }
        }
        private void Calendar_SizeChanged(object? sender, EventArgs e)
        {
            if (sender is null) { return; }
            UpdateRowHeight();
            UpdateColumnWidth();
        }

        public Calendar()
        {
            InitializeComponent();
            SizeChanged += Calendar_SizeChanged;
            _previousDay = DateTime.Today;
            _timer = new System.Timers.Timer(60000);
            _timer.Elapsed += Timer_Elapsed!;
            _timer.Start();
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (_previousDay.Date != DateTime.Now.Date)
            {
                if (Dates[(int)_previousDay.DayOfWeek].Equals(DateOnly.FromDateTime(_previousDay)))
                {
                    DateCalendarCell dateCalendarCell = (DateCalendarCell)calendarGridHead.Children.
                        First(dcc => dcc is DateCalendarCell date && date.Date.Equals(DateOnly.FromDateTime(_previousDay)));
                    if (dateCalendarCell is not null)
                    {
                            dateCalendarCell.CellColor= DarkBlueTheme.EerieBlack;
                    }
                }
                _previousDay = DateTime.Now;
                if (Dates[(int)_previousDay.DayOfWeek].Equals(DateOnly.FromDateTime(_previousDay)))
                {
                    DateCalendarCell dateCalendarCell = (DateCalendarCell)calendarGridHead.Children.
                        First(dcc => dcc is DateCalendarCell date && date.Date.Equals(DateOnly.FromDateTime(_previousDay)));
                    if (dateCalendarCell is not null)
                    {
                        dateCalendarCell.CellColor = DarkBlueTheme.LightSkyBlue;
                    }
                }
            }
        }
    }
}
