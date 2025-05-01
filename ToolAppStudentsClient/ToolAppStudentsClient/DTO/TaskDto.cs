using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolAppStudentsClient.DTO
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
        [Required]
        public TimeSpan Notification { set; get; }
        public string Description { set; get; }
        [Required]
        public string TaskColor { set; get; }
    }
}
