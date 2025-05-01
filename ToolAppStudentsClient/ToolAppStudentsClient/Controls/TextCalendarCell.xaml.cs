using Microsoft.Maui.Graphics.Converters;
using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolAppStudentsClient.Controls
{
    public partial class TextCalendarCell:ContentView
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text), typeof(string), typeof(TaskCalendarCell), string.Empty, BindingMode.TwoWay);
        public static readonly BindableProperty CellColorProperty = BindableProperty.Create(
            nameof(CellColor), typeof(Color), typeof(DateCalendarCell), Colors.Gray);
        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            nameof(TextColor), typeof(Color), typeof(DateCalendarCell), Colors.Black);
        public string Text
        {
            set => SetValue(TextProperty, value);
            get => (string)GetValue(TextProperty);
        }
        [TypeConverter(typeof(ColorTypeConverter))]
        public Color CellColor
        {
            set => SetValue(CellColorProperty, value);
            get => (Color)GetValue(CellColorProperty);
        }
        [TypeConverter(typeof(ColorTypeConverter))]
        public Color TextColor
        {
            set => SetValue(TextColorProperty, value);
            get => (Color)GetValue(TextColorProperty);
        }
        public TextCalendarCell()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
