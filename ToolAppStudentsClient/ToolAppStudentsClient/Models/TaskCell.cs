using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToolAppStudentsClient.Models
{
    public class TaskCell:GridCell
    {
        public string Title { get; set; }
        public DateOnly Day { get; set; }
        public TimeOnly Start { set; get; }
        public TimeOnly End { set; get; }
        public TimeSpan Duration { set; get; }
        public TimeSpan Notification { set; get; }
        public string Description { set; get; }
        public Color TaskColor { set; get; }
        public List<Task> Subtasks { set; get; }
        public int RowSpan { set; get; }
        public RowDefinitionCollection RowDefinitions { set; get; }
        public ColumnDefinitionCollection ColumnDefinitions { set; get; }
        public bool IsVisible { set; get; }
    }
}
