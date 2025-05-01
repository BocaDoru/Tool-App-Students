using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolAppStudentsClient.Models;

namespace ToolAppStudentsClient.Selectors
{
    public class CellTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EmptyCellTemplate { get; set; }
        public DataTemplate TaskCellTemplate { get; set; }
        public DataTemplate LogoutCellTemplate { get; set; }
        public DataTemplate InfoCellTemplate { get; set; }
        public DataTemplate DayCellTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is EmptyCell)
                return EmptyCellTemplate;
            else if (item is TaskCell)
                return TaskCellTemplate;
            else if(item is LogoutCell)
                return LogoutCellTemplate;
            else if(item is InfoCell)
                return InfoCellTemplate;
            else if(item is DayCell)
                return DayCellTemplate;
            return null;
        }
    }
}
