using Microsoft.Maui.Graphics.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolAppStudentsClient.ColorsTheme;

namespace ToolAppStudentsClient.Controls
{
    public partial class DateCalendarCell:ContentView
    {
        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            nameof(Date), typeof(DateOnly), typeof(DateCalendarCell), new DateOnly(), BindingMode.TwoWay, propertyChanged:DatePropertyChanged);

        public static readonly BindableProperty CellColorProperty = BindableProperty.Create(
            nameof(CellColor), typeof(Color), typeof(DateCalendarCell), DarkBlueTheme.EerieBlack, BindingMode.TwoWay);
        private static void DatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            DateCalendarCell cell = (DateCalendarCell)bindable;
            if(cell is not null)
            if (cell.Date.Equals(DateOnly.FromDateTime(DateTime.Today)))
                    cell.CellColor = DarkBlueTheme.LightSkyBlue;
        }

        [TypeConverter(typeof(DateOnlyConverter))]
        public DateOnly Date
        {
            set => SetValue(DateProperty, value);
            get => (DateOnly)GetValue(DateProperty);
        }
        [TypeConverter(typeof(ColorTypeConverter))]
        public Color CellColor
        {
            set => SetValue(CellColorProperty, value);
            get => (Color)GetValue(CellColorProperty);
        }
        public DateCalendarCell()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
