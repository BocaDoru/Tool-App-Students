using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Windows.Input;

namespace ToolAppStudentsClient.Controls;

public partial class TaskCalendarCell : ContentView
{

    public static readonly BindableProperty LeftOffsetProperty = BindableProperty.Create(
        nameof(LeftOffset), typeof(GridLength), typeof(TaskCalendarCell), new GridLength(0, GridUnitType.Star), BindingMode.TwoWay, propertyChanged: OnColumnHeightChanged);

    public static readonly BindableProperty TaskWidthProperty = BindableProperty.Create(
        nameof(TaskWidth), typeof(GridLength), typeof(TaskCalendarCell), new GridLength(1, GridUnitType.Star), BindingMode.TwoWay, propertyChanged: OnColumnHeightChanged);

    public static readonly BindableProperty RightOffsetProperty = BindableProperty.Create(
        nameof(RightOffset), typeof(GridLength), typeof(TaskCalendarCell), new GridLength(0, GridUnitType.Star), BindingMode.TwoWay, propertyChanged: OnColumnHeightChanged);

    public static readonly BindableProperty TaskColorProperty = BindableProperty.Create(
        nameof(TaskColor), typeof(Color), typeof(TaskCalendarCell), Colors.AliceBlue, BindingMode.TwoWay);

    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title), typeof(string), typeof(TaskCalendarCell), string.Empty, BindingMode.TwoWay);

    public static readonly BindableProperty DayProperty = BindableProperty.Create(
        nameof(Day), typeof(DateOnly), typeof(TaskCalendarCell), DateOnly.MinValue, BindingMode.TwoWay, propertyChanged:OnDayPropertyChanged);

    public static readonly BindableProperty StartHourProperty = BindableProperty.Create(
        nameof(StartHour), typeof(TimeOnly), typeof(TaskCalendarCell), TimeOnly.MinValue, BindingMode.TwoWay, propertyChanged: OnHourPropertyChanged);

    public static readonly BindableProperty EndHourProperty = BindableProperty.Create(
        nameof(EndHour), typeof(TimeOnly), typeof(TaskCalendarCell), TimeOnly.MaxValue, BindingMode.TwoWay, propertyChanged: OnHourPropertyChanged);

    public static readonly BindableProperty TaskLocationProperty = BindableProperty.Create(
        nameof(TaskLocation), typeof(string), typeof(TaskCalendarCell), string.Empty, BindingMode.TwoWay);

    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description), typeof(string), typeof(TaskCalendarCell), string.Empty, BindingMode.TwoWay);

    private GridLength StartOffset { get; set; }

    private GridLength TaskHeight { get; set; }

    private GridLength EndOffset { get; set; }


    [TypeConverter(typeof(GridLengthTypeConverter))]
    public GridLength LeftOffset
    {
        get => (GridLength)GetValue(LeftOffsetProperty);
        set => SetValue(LeftOffsetProperty, value);
    }

    [TypeConverter(typeof(GridLengthTypeConverter))]
    public GridLength TaskWidth
    {
        get => (GridLength)GetValue(TaskWidthProperty);
        set => SetValue(TaskWidthProperty, value);
    }

    [TypeConverter(typeof(GridLengthTypeConverter))]
    public GridLength RightOffset
    {
        get => (GridLength)GetValue(RightOffsetProperty);
        set => SetValue(RightOffsetProperty, value);
    }

    public Color TaskColor
    {
        get => (Color)GetValue(TaskColorProperty);
        set => SetValue(TaskColorProperty, value);
    }
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    [TypeConverter(typeof(DateOnlyConverter))]
    public DateOnly Day
    {
        get => (DateOnly)GetValue(DayProperty);
        set => SetValue(DayProperty, value);
    }

    [TypeConverter(typeof(TimeOnlyConverter))]
    public TimeOnly StartHour
    {
        get => (TimeOnly)GetValue(StartHourProperty);
        set => SetValue(StartHourProperty, value);
    }

    [TypeConverter(typeof(TimeOnlyConverter))]
    public TimeOnly EndHour
    {
        get => (TimeOnly)GetValue(EndHourProperty);
        set => SetValue(EndHourProperty, value);
    }
    public string TaskLocation
    {
        get => (string)GetValue(TaskLocationProperty);
        set => SetValue(TaskLocationProperty, value);
    }
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static void OnDayPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is TaskCalendarCell task)
        {
            DateOnly day=task.Day;
            Grid.SetColumn(bindable,(int)day.DayOfWeek);
        }
    }
    public static TimeOnly RoundUp(TimeOnly time)
    {
        return time.AddMinutes((60 - time.Minute) % 60);
    }
    public static TimeOnly RoundDown(TimeOnly time)
    {
        return time.AddMinutes(-time.Minute);
    }
    public static void OnHourPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is TaskCalendarCell task)
        {
            TimeOnly startHour = task.StartHour;
            TimeOnly endHour = task.EndHour;
            if (startHour < endHour)
            {
                int row = startHour.Hour;
                Grid.SetRow(bindable, row);
                int rowSpan = (RoundUp(endHour) - RoundDown(startHour)).Hours;
                Grid.SetRowSpan(bindable, rowSpan);
                UpdateRowDefinitions(task, rowSpan);
            }
            else
            {
                Grid.SetRow(bindable, 0);
                Grid.SetRowSpan(bindable, 1);
            }
        }
    }

    private static void UpdateRowDefinitions(TaskCalendarCell control, int rowSpan)
    {
        control.StartOffset = new GridLength(control.StartHour.Minute / (rowSpan * 60.0), GridUnitType.Star);
        control.EndOffset = new GridLength((60 - control.EndHour.Minute) % 60 / (rowSpan * 60.0), GridUnitType.Star);
        control.TaskHeight = new GridLength((control.EndHour - control.StartHour).TotalMinutes / (rowSpan * 60.0), GridUnitType.Star);

        control.cellGrid.RowDefinitions.Clear();
        control.cellGrid.RowDefinitions.Add(new RowDefinition(control.StartOffset));
        control.cellGrid.RowDefinitions.Add(new RowDefinition(control.TaskHeight));
        control.cellGrid.RowDefinitions.Add(new RowDefinition(control.EndOffset));
    }

    private static void OnColumnHeightChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (TaskCalendarCell)bindable;
        control.UpdatenColumnDefinitions();
    }
    private void UpdatenColumnDefinitions()
    {
        TaskWidth = new GridLength(1.0 - LeftOffset.Value - RightOffset.Value, GridUnitType.Star);
        cellGrid.ColumnDefinitions.Clear();
        cellGrid.ColumnDefinitions.Add(new ColumnDefinition(LeftOffset));
        cellGrid.ColumnDefinitions.Add(new ColumnDefinition(TaskWidth));
        cellGrid.ColumnDefinitions.Add(new ColumnDefinition(RightOffset));
    }
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (cellGrid != null)
        {
            double absoluteTaskHeight = height * TaskHeight.Value;
            if (titleLabel is not null)
                titleLabel.IsVisible = absoluteTaskHeight > 21.8;
            if (hoursLabel is not null)
                hoursLabel.IsVisible = absoluteTaskHeight > 36.2;
            if (locationLabel is not null)
                locationLabel.IsVisible = absoluteTaskHeight > 50.6;
            if (descriptionLabel is not null)
                descriptionLabel.IsVisible = absoluteTaskHeight > 65;
        }
    }
    [RelayCommand]
    public void HandleTaskButton()
    {
        Application.Current.MainPage.DisplayAlert("Button Clicked", $"You clicked a task cell", "OK");
    }
    public TaskCalendarCell()
    {
        InitializeComponent();
        cellGrid.BindingContext = this;
    }
}