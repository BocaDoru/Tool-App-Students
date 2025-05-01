using Org.BouncyCastle.Utilities.Encoders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace ToolAppStudentsServer.Models
{
    public class CalendarTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan Start { set; get; }
        public TimeSpan End { set; get; }
        public TimeSpan Duration { set; get; }
        public TimeSpan Notification { set; get; }
        public string Description { set; get; }
        public byte[] Color { set; get; }
    }
}
