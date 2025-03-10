using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS_labs.Classes.Styles
{
    public class PointStyle
    {
        private Pen pointPen;
        public Pen PointPen { get { return pointPen; } }

        public PointStyle(Pen pen)
        { this.pointPen = pen; }

        public PointStyle()
        { pointPen = new Pen(Color.Blue, 5); }
    }
}
