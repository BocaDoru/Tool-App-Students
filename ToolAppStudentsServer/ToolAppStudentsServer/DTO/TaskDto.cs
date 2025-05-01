using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace ToolAppStudentsServer.DTO
{
    public class TaskDto
    {
        public string Name { get; set; }
        [Required]
        public DateTime Day { get; set; }
        [Required]
        public TimeSpan Start { set; get; }
        [Required]
        public TimeSpan End { set; get; }
        [Required]
        public TimeSpan Duration { set; get; }
        public TimeSpan Notification { set; get; }
        public string Description { set; get; }
        [Required]
        public string TaskColor { set; get; }
    }
}
