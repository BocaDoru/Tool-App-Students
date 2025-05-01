using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolAppStudentsClient.Models
{
    public class TaskModel
    {
        public string Name { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan Start { set; get; }
        public TimeSpan End { set; get; }
        public TimeSpan Duration { set; get; }
        public TimeSpan Notification { set; get; }
        public string Description { set; get; }
        public Color TaskColor { set; get; }
        public List<Task> Subtasks { set; get; }
    }
}
