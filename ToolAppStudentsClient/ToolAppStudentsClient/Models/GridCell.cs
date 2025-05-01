using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Graphics;

namespace ToolAppStudentsClient.Models
{
    public class GridCell
    {
        public string Text { get; set; }
        public Color BackgroundColor { get; set; }
        public int Row { get; set; }
        public int Column {  get; set; }
        public int ZIndex { set; get; }
    }
}
