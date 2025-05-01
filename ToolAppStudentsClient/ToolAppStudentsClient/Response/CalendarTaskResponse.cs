using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolAppStudentsClient.Response
{
    public class CalendarTaskResponse
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? Title { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan Start { set; get; }
        public TimeSpan End { set; get; }
        public TimeSpan Duration { set; get; }
        public TimeSpan? Notification { set; get; }
        public string? Description { set; get; }
        public byte[] Color { set; get; }
    }
}
