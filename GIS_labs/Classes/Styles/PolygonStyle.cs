using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_labs.Classes.Styles
{
    public class PolygonStyle
    {
        private Pen borderPen;
        public Pen BorderPen { get { return borderPen; } }

        private SolidBrush areaBrush;
        public SolidBrush AreaBrush { get { return areaBrush; } }

        public PolygonStyle(Pen pen, SolidBrush brush)
        {
            this.borderPen = pen;
            this.areaBrush = brush;
        }

        public PolygonStyle()
        {
            borderPen = new Pen(Color.Black, 3);
            areaBrush = new SolidBrush(Color.LightGray);
        }
    }
}
